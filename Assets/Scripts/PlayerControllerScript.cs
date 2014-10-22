using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;

	private bool facingRight = true;
	private ArrayList enemiesWithinAttackRange = new ArrayList();

	private bool paaPoikki = false;
	private bool jalkaPoikki = false;
	private bool ottiOsumaa = false;
	private bool attacking = false;
	private bool powerattacking = false;
	private float ottiOsumaaTimer = 0f;
	private float attackTimer=0f;

	public Slider healthSlider;
	public Image limited;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.8f);
	
	Animator anim;
	bool valahdys = false;

	//Stats
	public int health;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

	}

	void Update () {
		if (attacking || powerattacking) {
			attackTimer+=Time.deltaTime;
				}

		if (Input.GetKeyDown ("space") && !powerattacking && !attacking) {
						anim.SetTrigger ("Attack");
						attacking = true;
				}

		if (Input.GetKeyDown ("left ctrl") && !attacking && !powerattacking) {
			anim.SetTrigger ("PowerAttack");
			powerattacking = true;
			//moveHorizontal = 10f;
			//anim.SetFloat ("Speed", Mathf.Abs (40.0f));
		}

		if (attacking && attackTimer>0.20f) {
			foreach(GameObject enemy in enemiesWithinAttackRange) {
				if(IsOnSameVerticalLevelWithEnemy(enemy)) {
					InflictDamageToEnemy(enemy, 20);
				}
			}
			attacking = false;
			attackTimer=0.0f;
		}

		if (powerattacking && attackTimer>0.50f) {
			foreach(GameObject enemy in enemiesWithinAttackRange) {
				if(IsOnExactVerticalLevelWithEnemy(enemy)) {
					DecapitateEnemy(enemy);
				}
				else if(IsOnSameVerticalLevelWithEnemy(enemy)) {
					InflictDamageToEnemy(enemy, 1000);
				}
			}
			powerattacking = false;
			attackTimer=0.0f;
		}

		if(valahdys) {
			limited.color = flashColour;
		} else {
			limited.color = Color.Lerp (limited.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		valahdys = false;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (powerattacking) {
						if (facingRight) {
								moveHorizontal = 1.33f;
						} else {
								moveHorizontal = -1.33f;
						}
				}
	    if (attacking) {

					moveHorizontal = moveHorizontal/2;
					
				}
			

		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Base.PowerAttack")) {
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

		if (ottiOsumaa) {
			ottiOsumaaTimer += Time.deltaTime;
			if (ottiOsumaaTimer > 0.3f) {
				//Destroy (GameObject.FindWithTag ("verisuihku"));
				ottiOsumaa=false;
			}
		}

		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "EnemyShape") {


			enemiesWithinAttackRange.Add(col.transform.parent.gameObject);
		}

	 	if (col.gameObject.tag == "KaannyTyhma") {
			col.transform.parent.gameObject.SendMessage("Flip");

		
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "EnemyShape") {
			enemiesWithinAttackRange.Remove(col.transform.parent.gameObject);
		}
	}

	void ApplyDamage(int damage) {

		health -= damage;

		healthSlider.value = health;
		valahdys = true;

		var number = Random.Range(10f,20f);

		if (health > 0f) {
			gameObject.GetComponentInChildren<VeriLentaa> ().suihkauta ();
			ottiOsumaaTimer=0f;
			ottiOsumaa=true;
		}
		
		else if (!paaPoikki && number < 15f) {
			anim.SetBool ("chopOffHead", true);
			gameObject.GetComponentInChildren<MestausScript> ().katkaise ();
			paaPoikki = true;
			jalkaPoikki=true;
		}
		
		else if (!jalkaPoikki && number >= 15f) {
			anim.SetBool ("damageLeg", true);
			//gameObject.GetComponentInChildren<MestausScript> ().katkaise ();
			gameObject.GetComponentInChildren<RampautusScript> ().katkaise ();
			jalkaPoikki=true;
			paaPoikki = true;
		}
	}

	void InflictDamageToEnemy(GameObject enemy, int damage) {
		enemy.SendMessage("ApplyDamage", 20);
	}

	void DecapitateEnemy(GameObject enemy) {
		enemy.SendMessage("Decapitate");
	}

	bool IsOnSameVerticalLevelWithEnemy(GameObject enemy) {
		float yDifference = (enemy.transform.position.y - this.transform.position.y);
		
		if (Mathf.Abs(yDifference) > 0.2f) {
			return false;
		} else {
			return true;
		}
	}

	bool IsOnExactVerticalLevelWithEnemy(GameObject enemy) {
		float yDifference = (enemy.transform.position.y - this.transform.position.y);
		
		if (Mathf.Abs(yDifference) > 0.05f) {
			return false;
		} else {
			return true;
		}
	}

	public bool isHeroAlive() {
		if (health <= 0) {
						return false;
				}
		return true;
		}

}
