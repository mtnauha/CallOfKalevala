using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;

	private bool facingRight = true;
	private ArrayList enemiesWithinAttackRange = new ArrayList();

	Animator anim;

	//Stats
	public int health;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetKeyDown ("space")) {
			anim.SetTrigger ("Attack");

			foreach(GameObject enemy in enemiesWithinAttackRange) {
				if(IsOnSameVerticalLevelWithEnemy(enemy)) {
					InflictDamageToEnemy(enemy, 20);
				}
			}
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Attack")) {
			anim.SetFloat ("Speed", Mathf.Abs (moveHorizontal + moveVertical));
			rigidbody2D.velocity = new Vector2 (moveHorizontal * maxSpeed, moveVertical * maxSpeed);
		} else {
			anim.SetFloat ("Speed", 0);
			rigidbody2D.velocity = Vector2.zero;
		}

		if (moveHorizontal > 0 && !facingRight)
			Flip ();
		else if (moveHorizontal < 0 && facingRight)
			Flip ();

		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy") {
			enemiesWithinAttackRange.Add(col.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy") {
			enemiesWithinAttackRange.Remove(col.gameObject);
		}
	}

	void ApplyDamage(int damage) {
		health -= damage;
	}

	void InflictDamageToEnemy(GameObject enemy, int damage) {
		enemy.SendMessage("ApplyDamage", 20);
	}

	bool IsOnSameVerticalLevelWithEnemy(GameObject enemy) {
		float yDifference = (enemy.transform.position.y - this.transform.position.y);
		
		if (Mathf.Abs(yDifference) > 0.2f) {
			return false;
		} else {
			return true;
		}
	}

}
