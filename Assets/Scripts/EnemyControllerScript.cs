using UnityEngine;
using System.Collections;

public class EnemyControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	public float maxAttackCooldown = 2f;

	private bool facingRight = false;
	private bool enemyWithinAttackRange = false;
	private bool dead;
	private float moveHorizontal = 0;
	private float moveVertical = 0;
	private float attackCooldown;
	private bool headChopped;

	Animator anim;
	GameObject hero;

	//Stats
	public int health;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		hero = GameObject.FindWithTag ("Hero");
		health = 100;
		attackCooldown = 0;
		dead = false;
		headChopped = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Update is called once per frame
	void FixedUpdate () {

		moveHorizontal = 0;
		moveVertical = 0;

		if (!dead) {
			attackCooldown -= Time.deltaTime;

			if (enemyWithinAttackRange && IsOnSameVerticalLevelWithHero()) {
				if (attackCooldown <= 0) {
					anim.SetTrigger ("Attack");
					attackCooldown = maxAttackCooldown;
				}
			}

			//Liikutetaan vihollista pelaajan suuntaan, jos ei olla oikealla etäisyydellä tai syvyydellä
			if (!enemyWithinAttackRange || !IsOnSameVerticalLevelWithHero()) {
				if ((hero.transform.position.x - this.transform.position.x) > 0.2f) {
					moveHorizontal = 1;
				} else if ((hero.transform.position.x - this.transform.position.x) < 0) {
					moveHorizontal = -1;
				} else {
					moveHorizontal = 0;
				}
				
				if ((hero.transform.position.y - this.transform.position.y) > 0.2f) {
					moveVertical = 1;
				} else if ((hero.transform.position.y - this.transform.position.y) < 0) {
					moveVertical = -1;
				} else {
					moveVertical = 0;
				}
			}
			
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Orc_attack") &&
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
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void ApplyDamage(int damage) {
		if (!dead) {
						health -= damage;

						if (health <= 0) {
								SetDead ();


				
								var number = Random.Range (10f, 20f);
								if (number < 15f && !headChopped) {
										anim.SetTrigger ("HeadChop");
										headChopped = true;
										gameObject.GetComponentInChildren<MestausScript> ().katkaiseOrc ();
								} else {
										gameObject.GetComponentInChildren<VeriLentaa> ().suihkauta ();
										anim.SetTrigger ("Die");
								}

						} else {
								anim.SetTrigger ("Damage");
								if (health > 0f) {
										gameObject.GetComponentInChildren<VeriLentaa> ().suihkauta ();
										//ottiOsumaaTimer=0f;
										//ottiOsumaa=true;
								}
						}
				} 
	}

	void inflictDamageToPlayer(int damage) {
		hero.SendMessage("ApplyDamage", damage);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Hero") {
			enemyWithinAttackRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Hero") {
			enemyWithinAttackRange = false;
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
	}
}
