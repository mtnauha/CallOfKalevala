using UnityEngine;
using System.Collections;

public class EnemyControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	public float maxAttackCooldown = 2f;
	public float attackTimer = 0f;

	private bool facingRight = false;
	private bool enemyWithinAttackRange = false;
	private bool dead;
	private float moveHorizontal = 0;
	private float moveVertical = 0;
	private float timer = 0;
	private float attackCooldown;
	private bool damageApplied = true;

	//private bool attackInProgress = false;
	//private bool attackFinished = false;

	Animator anim;
	GameObject hero;

 	
	//Stats
	private float health;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		hero = GameObject.FindWithTag ("Hero");
		health = 100f;
		attackCooldown = 0;
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Update is called once per frame
	void FixedUpdate () {

		moveHorizontal = 0;
		moveVertical = 0;

		if (health <= 0 && !dead) {
			anim.SetTrigger ("Die");
			dead = true;
		}

		if (!dead) {
			//Liikutetaan vihollista pelaajan suuntaan, jos ei olla hyökkäämässä
			if (!enemyWithinAttackRange) {
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

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Base.Orc_attack")) {
				attackTimer += Time.deltaTime;
			} else {attackTimer=0f;}
			
			if (enemyWithinAttackRange) {
				if (attackCooldown <= 0) {
					anim.SetTrigger ("Attack");
					damageApplied=false;

					//attackInProgress = true;
					//attackFinished = false;
					attackCooldown = maxAttackCooldown;
				} else {
					attackCooldown -= Time.deltaTime;
				}
			}

			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Base.Orc_attack")) {

				anim.SetFloat ("Speed", Mathf.Abs (moveHorizontal + moveVertical));
				rigidbody2D.velocity = new Vector2 (moveHorizontal * maxSpeed, moveVertical * maxSpeed);
			} else {

				if (attackTimer>0.8f && enemyWithinAttackRange && !damageApplied) {
					inflictDamageToPlayer(10f);
					Debug.Log ("Enemy damaged the player!");
					damageApplied=true;
					}

				anim.SetFloat ("Speed", 0);
				rigidbody2D.velocity = Vector2.zero;
			}
			
			if (moveHorizontal > 0 && !facingRight)
				Flip ();
			else if (moveHorizontal < 0 && facingRight)
				Flip ();
		}

	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void ApplyDamage(float damage) {
		health -= damage;
		anim.SetTrigger ("Damage");
	}

	void inflictDamageToPlayer(float damage) {
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
}
