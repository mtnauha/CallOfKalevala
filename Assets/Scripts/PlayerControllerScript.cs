using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	private bool facingRight = true;
	private bool attack = false;
	private bool paaPoikki = false;
	private bool jalkaPoikki = false;
	private bool ottiOsumaa = false;
	private float ottiOsumaaTimer = 0f;
	//GameObject HeadLocation;

	Animator anim;

	//Stats
	private float health = 50f;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	//	HeadLocation = GetComponentsInChildren<HeadLocation> ();
	}

	void Update () {
		if (Input.GetKeyDown ("space")) {
			anim.SetTrigger ("Attack");
			attack = true;
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


		if (ottiOsumaa) {
						ottiOsumaaTimer += Time.deltaTime;
						if (ottiOsumaaTimer > 0.3f) {
						//Destroy (GameObject.FindWithTag ("verisuihku"));
				ottiOsumaa=false;
						}
				}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy" && attack) {
			col.gameObject.SendMessage("ApplyDamage", 20);
			attack = false;
			Debug.Log ("Enemy collision!");
		}
	}

	public void ApplyDamage(float damage) {
		health -= damage;
		//anim.SetBool ("damageLeg", true);
		Debug.Log ("damage was applied!");
		//GameObject kaula = GameObject.FindWithTag ("HeadLocation");
		//kaula.SendMessage ("Katkaise");
		//MestausScript mestaus = GetComponent<MestausScript>();
		//mestaus.katkaise ();
		//gameObject.collider2D.enabled = false;
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

}
