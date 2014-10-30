using UnityEngine;
using System.Collections;

public class EnemyControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	public float maxFastAttackCooldown = 1.00f;
	public float maxAttackCooldown = 2f;

	private bool facingRight = false;
	private bool enemyWithinAttackRange = false;
	private bool dead;
	private float moveHorizontal = 0;
	private float moveVertical = 0;
	private float attackCooldown;
	private bool headChopped;
	private bool legsChopped;
	private bool armChopped;
	private bool oneToBeSure;

	public Transform Enemy;


	Animator anim;
	public GameObject hero;

	//Stats
	public int health;

	//sound
	public AudioSource[] sounds;
	public AudioSource pain1;
	public AudioSource pain2;
	public AudioSource pain3;
		
	public AudioSource death;
	public AudioSource hit;
	public AudioSource legs;
	public AudioSource swing;

	public AudioSource squirt;
	public AudioSource deathhead;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//hero = GameObject.FindGameObjectWithTag ("Hero");
		health = 100;
		attackCooldown = 0;
		dead = false;
		headChopped = false;
		legsChopped = false;
		armChopped = false;
		oneToBeSure = false;

		sounds = GetComponents<AudioSource>();
		pain1 = sounds[0];
		pain2 = sounds[1];
		pain3 = sounds[2];

		death = sounds[3];
		hit = sounds[4];
		legs = sounds[5];
		swing = sounds[6];
		
		squirt = sounds[7];
		deathhead = sounds[8];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Update is called once per frame
	void FixedUpdate () {

		float oldX = this.transform.position.x;

		moveHorizontal = 0;
		moveVertical = 0;
		death.pitch = Random.Range(0.8f, 1.2f); death.volume = Random.Range(0.75f, 1.0f);
		swing.pitch = Random.Range(0.9f, 1.1f); swing.volume = Random.Range(0.75f, 1.0f);
		hit.pitch = Random.Range(0.8f, 1.2f); hit.volume = Random.Range(0.75f, 1.0f);

		if (!dead) {
			attackCooldown -= Time.deltaTime;

			if (enemyWithinAttackRange && IsOnSameVerticalLevelWithHero() && IsNearHorizontallLevelWithHero()) {
				if (attackCooldown <= 0 && !oneToBeSure) {
					if (!hero.GetComponent<PlayerControllerScript>().isHeroAlive()) {oneToBeSure=true; hit.PlayDelayed(0.25f);}
					anim.SetTrigger ("FastAttack");
					swing.Play ();
					attackCooldown = maxFastAttackCooldown;
				}
			}

			else if (enemyWithinAttackRange && IsOnSameVerticalLevelWithHero()) {
				if (attackCooldown <= 0 && !oneToBeSure) {
					if (!hero.GetComponent<PlayerControllerScript>().isHeroAlive()) {oneToBeSure=true; hit.PlayDelayed(0.40f);}
					anim.SetTrigger ("Attack");
					swing.Play ();
					attackCooldown = maxAttackCooldown;
				}
			}

			//Liikutetaan vihollista pelaajan suuntaan, jos ei olla oikealla etäisyydellä tai syvyydellä
			if (!enemyWithinAttackRange || !IsOnSameVerticalLevelWithHero()) {
				float xIncrease = 2f;
				
				if(hero.transform.position.x - this.transform.position.x >= 0) {
					xIncrease = -2f;
				}
				
				Vector3 targetPosition = new Vector3(hero.transform.position.x + xIncrease, hero.transform.position.y, hero.transform.position.z);

				if(hero.transform.position.z > this.transform.position.z) {
					Debug.Log("Pelaajan koordinaatit: z: " + hero.transform.position.z);
					Debug.Log("Omat koordinaatit: z: " + this.transform.position.z);
				}

				if ((targetPosition.x - this.transform.position.x) > 0.2f) {
					moveHorizontal = 1;
				} else if ((targetPosition.x - this.transform.position.x) < 0) {
					moveHorizontal = -1;
				} else {
					moveHorizontal = 0;
				}
				
				if ((targetPosition.y - this.transform.position.y) > 0.2f) {
					moveVertical = 1;
				} else if ((targetPosition.y - this.transform.position.y) < 0) {
					moveVertical = -1;
				} else {
					moveVertical = 0;
				}
			}
			
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Orc_attack") &&
			    !anim.GetCurrentAnimatorStateInfo (0).IsName ("orc_fast_attack") &&
			    !anim.GetCurrentAnimatorStateInfo (0).IsName ("Orc_damage")) {

				if(Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical)) {
					anim.SetFloat ("Speed", Mathf.Abs (moveHorizontal));
				} else {
					anim.SetFloat ("Speed", Mathf.Abs (moveVertical));
				}
				rigidbody2D.velocity = new Vector2 (moveHorizontal * maxSpeed, moveVertical * maxSpeed);
			} else {
				anim.SetFloat ("Speed", 0);
				rigidbody2D.velocity = Vector2.zero;
			}
			
			if (moveHorizontal > 0 && !facingRight)
				Flip ();
			else if (moveHorizontal < 0 && facingRight)
				Flip ();

			//Asetetaan syvyys objektin korkeuden mukaan, jotta pelissä näkyvät spritet piirtyvät ruudulle oikeassa järjestyksessä
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
		}
	}

	void Flip() {
		if (!dead) {
						Vector3 theScale = transform.localScale;
						if(facingRight && theScale.x <= 0) {
							theScale.x *= -1;
						}
						facingRight = !facingRight;
						theScale.x *= -1;
						transform.localScale = theScale;
				}
	}

	void Legcapitate() {
		if (!headChopped && !legsChopped && health>0) {
			health = -50;
			hit.Play ();
			anim.SetTrigger ("DieLegs");
			legsChopped = true;
			death.Play ();
			legs.Play ();
			gameObject.GetComponentInChildren<MestausScript> ().katkaiseOrcJalat ();
			SetDead ();
		}
	}

	void Decapitate() {
		if (!headChopped && !legsChopped && health>0) {
						health = -50;
				
						anim.SetTrigger ("HeadChop");
						headChopped = true;
						gameObject.GetComponentInChildren<MestausScript> ().katkaiseOrc ();
						SetDead ();
						deathhead.Play ();
						//hit.Play ();
						squirt.Play ();

				}
		}

	void ApplyDamage(int damage) {
		if (!dead && health>0) {
			health -= damage;
			hit.Play ();

			if (health <= 0) {


				var number = Random.Range (10f, 40f);
				if (number < 13f && !headChopped && !armChopped) {
					anim.SetTrigger ("HeadChop");
					headChopped = true;
					gameObject.GetComponentInChildren<MestausScript> ().katkaiseOrc ();
					deathhead.Play ();
					squirt.Play ();


				} else if (number < 18f && !headChopped && !armChopped) {
					anim.SetTrigger ("ArmChop");

					Flip ();
					//squirt.Play ();
					death.Play();

					armChopped = true;
					gameObject.GetComponentInChildren<MestausScript> ().katkaiseOrcKasi ();
				} else if (number < 25f && !headChopped && !armChopped) {
					gameObject.GetComponentInChildren<VeriLentaa> ().suihkauta ();
					anim.SetTrigger ("DieHalves");
					//squirt.Play ();
					death.Play();
				} else {
					gameObject.GetComponentInChildren<VeriLentaa> ().suihkauta ();
					anim.SetTrigger ("Die");
					death.Play();
				}
				SetDead ();
			} else {
				anim.SetTrigger ("Damage");
				if (health > 0f) {

					var pain = Random.Range (0f, 31f);
					
					if (pain<11f) { pain1.pitch = Random.Range(0.8f, 1.2f); pain1.volume = Random.Range(0.6f, 0.9f); pain1.Play(); }
					else if (pain<20f) { pain2.pitch = Random.Range(0.8f, 1.2f); pain2.volume = Random.Range(0.6f, 0.9f);pain2.Play();}
					else { pain3.pitch = Random.Range(0.8f, 1.2f); pain3.volume = Random.Range(0.6f, 0.9f);pain3.Play();}

					gameObject.GetComponentInChildren<VeriLentaa> ().suihkauta ();
				}
			}
		} 
	}

	void inflictDamageToPlayer(int damage) {
		if (IsOnSameVerticalLevelWithHero() && enemyWithinAttackRange) {
			hero.SendMessage("ApplyDamage", damage);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "HeroShape") {
			enemyWithinAttackRange = true;
		}
			
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "HeroShape") {
			enemyWithinAttackRange = false;
		}

	}

	bool IsNearHorizontallLevelWithHero() {
		float xDifference = (hero.transform.position.x - this.transform.position.x);
		
		if (Mathf.Abs(xDifference) > 2.5f) {
			return false;
		} else {
			return true;
		}
	}


	bool IsOnSameVerticalLevelWithHero() {
		float yDifference = (hero.transform.position.y - this.transform.position.y);

		if (Mathf.Abs(yDifference) > 0.2f) {
			return false;
		} else {
			return true;
		}
	}

	void SetDead() {
		dead = true;

		anim.SetBool ("Dead", true);

		rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.None;
		collider2D.enabled = false;
		anim.SetFloat ("Speed", 0);
		rigidbody2D.velocity = Vector2.zero;

		transform.position = new Vector3 (transform.position.x, transform.position.y, 10);

		PlayerControllerScript script = hero.GetComponent<PlayerControllerScript>();
		script.IncreaseKillCount ();

		/*
		var newenemy = Instantiate (Enemy) as Transform;
		var xMod = Random.Range (-5.5f, 5.5f);
		newenemy.position = transform.position;
		newenemy.position = new Vector3 (0f+xMod, 0f, 0f);
		*/
	}

	public int getHealth() {
				return health;
		}
}
