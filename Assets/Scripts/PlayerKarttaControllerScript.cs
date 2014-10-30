using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerKarttaControllerScript : MonoBehaviour
{

		public float maxSpeed = 10f;
		private bool facingRight = true;
		private ArrayList enemiesWithinAttackRange = new ArrayList ();
		private bool paaPoikki = false;
		private bool jalkaPoikki = false;
		private bool ottiOsumaa = false;
		private bool attacking = false;
		private bool highAttacking = false;
		private bool powerattacking = false;
		private bool legattacking = false;
		private float ottiOsumaaTimer = 0f;
		private float attackTimer = 0f;
		private float cooldown = 0f;
		private float legcooldown = 0f;
		private bool dead;
		private bool victory = false;
		public Slider healthSlider;
		public Image limited;
		public float flashSpeed = 5f;
		public Color flashColour = new Color (1f, 0f, 0f, 0.8f);
		bool nowBlocking = false;
		int blockCounter = 0;
		Animator anim;
		bool valahdys = false;
		private bool hyokkaajanSuunta;
		private bool wantsToBlock = false;
	private bool saimaa = false;
	private bool ranta = false;
	private bool forest = false;

		//Stats
		public int health;
		public int killCount;

		//sound
		public AudioSource[] sounds;
		public AudioSource pain1;
		public AudioSource pain2;
		public AudioSource pain3;
		public AudioSource pain4;
		public AudioSource death;
		public AudioSource hit;
		public AudioSource swing;
		public AudioSource clang;
		public AudioSource squirt;


		// Use this for initialization
		void Start ()
		{
				killCount = 0;
				dead = false;
				anim = GetComponent<Animator> ();

				sounds = GetComponents<AudioSource> ();
				pain1 = sounds [0];
				pain2 = sounds [1];
				pain3 = sounds [2];
				pain4 = sounds [3];

				death = sounds [4];
				hit = sounds [5];
				swing = sounds [6];
				clang = sounds [7];

				squirt = sounds [8];


		}

		void Update ()
		{
				if (health > 0 && !victory) {
						legcooldown -= Time.deltaTime;
						if (legcooldown < 0.0f) {
								anim.SetBool ("moreLegAttack", false);
						}

						if (attacking || powerattacking || legattacking || highAttacking) {
								attackTimer += Time.deltaTime;
								if (Input.GetKeyDown ("f") && (attackTimer > 0.19f)) {
										wantsToBlock = true;
								}
						} else {
								cooldown -= Time.deltaTime;

						}

						
						if (Input.GetKeyDown ("space") && (forest || saimaa || ranta)) {
							if (saimaa) {
								Application.LoadLevel ("Saimaa");
							}
							
							if (ranta) {
								Application.LoadLevel ("Ranta");
							}
							
							if (forest) {
								Application.LoadLevel ("Forest");
							}
						}

						if (legattacking && attackTimer > 0.30f) {
								swing.Play ();
								foreach (GameObject enemy in enemiesWithinAttackRange) {
										if (IsOnSameVerticalLevelWithEnemy (enemy)) {
								
												EnemyControllerScript ecs = enemy.GetComponent<EnemyControllerScript> ();
												if (ecs.getHealth () <= 25) {
														//if (enemy.SendMessage("getHealth")<=25) {
														LegcapitateEnemy (enemy);
												} else {
														InflictDamageToEnemy (enemy, 25);
												}
										}
								}
								legattacking = false;
								attackTimer = 0.0f;
						}	

						if ((highAttacking && attackTimer > 0.25f) || (attacking && attackTimer > 0.20f)) {
								swing.Play ();
								foreach (GameObject enemy in enemiesWithinAttackRange) {
										
										if (IsOnSameVerticalLevelWithEnemy (enemy) && attacking) {
												InflictDamageToEnemy (enemy, 20);
										}
										if (IsOnSameVerticalLevelWithEnemy (enemy) && highAttacking && !IsTooCloseToEnemy (enemy)) {
												InflictDamageToEnemy (enemy, 20);
										}
								}
								attacking = false;
								highAttacking = false;
								attackTimer = 0.0f;
						}

						if (powerattacking && attackTimer > 0.50f) {
								swing.Play ();
								foreach (GameObject enemy in enemiesWithinAttackRange) {
										if (IsOnExactVerticalLevelWithEnemy (enemy) && !IsTooCloseToEnemy (enemy)) {
												DecapitateEnemy (enemy);
										}
										//} else if (IsOnLegVerticalLevelWithEnemy (enemy)) {
										//		LegcapitateEnemy (enemy);
										//}
										else if (IsOnSameVerticalLevelWithEnemy (enemy)) {
												if (IsTooCloseToEnemy (enemy)) {
														InflictDamageToEnemy (enemy, 33);
												} else {
														InflictDamageToEnemy (enemy, 75);
												}
										}
								}
								powerattacking = false;
								attackTimer = 0.0f;
						}

						if (valahdys) {
								limited.color = flashColour;
						} else {
								limited.color = Color.Lerp (limited.color, Color.clear, flashSpeed * Time.deltaTime);
						}
						valahdys = false;
				}
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				swing.pitch = Random.Range (0.9f, 1.1f);
				swing.volume = Random.Range (0.75f, 1.0f);
				hit.pitch = Random.Range (0.8f, 1.2f);
				hit.volume = Random.Range (0.75f, 1.0f);
				clang.pitch = Random.Range (0.9f, 1.1f);
				clang.volume = Random.Range (0.45f, 0.6f);

				if (health <= 0) {
						dead = true;
						anim.SetBool ("herodead", true);
						gameObject.rigidbody2D.velocity = Vector3.zero;
						gameObject.rigidbody2D.isKinematic = true;
						gameObject.collider2D.enabled = false;
				}

				if (health > 0 && !victory) {
						float moveHorizontal = Input.GetAxis ("Horizontal");
						float moveVertical = Input.GetAxis ("Vertical");

						if (powerattacking) {
								if (facingRight) {
										moveHorizontal = 2.22f;
										moveVertical = 0f;
								} else {
										moveHorizontal = -2.22f;
										moveVertical = 0f;
								}
						}
						if (highAttacking) {
				
								if (facingRight) {
										moveHorizontal = 1.83f;
										moveVertical = 0.0f;
								} else {
										moveHorizontal = -1.83f;
										moveVertical = 0.0f;
								}
					
						}

						if (attacking) {

								moveHorizontal = 0f;
								moveVertical = 0f;
					
						}
						if (legattacking) {
			
								if (facingRight) {
										moveHorizontal = 2.03f;
										moveVertical = 0.0f;
								} else {
										moveHorizontal = -2.03f;
										moveVertical = 0.0f;
								}
			
						}
			

						if (health > 0 && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Base.Attack") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Base.hero_leg_attack") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Base.PowerAttack") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Base.hero_block_hold")) {
								if (Mathf.Abs (moveHorizontal) > Mathf.Abs (moveVertical)) {
										anim.SetFloat ("Speed", Mathf.Abs (moveHorizontal));
								} else {
										anim.SetFloat ("Speed", Mathf.Abs (moveVertical));
								}
								rigidbody2D.velocity = new Vector2 (moveHorizontal * maxSpeed, moveVertical * maxSpeed);
						} else {
								anim.SetFloat ("Speed", 0);
								rigidbody2D.velocity = Vector2.zero;
						}

						if (attackTimer < 0.1f && health > 0 && moveHorizontal > 0 && !facingRight)
								Flip ();
						else if (attackTimer < 0.1f && health > 0 && moveHorizontal < 0 && facingRight)
								Flip ();

						if (ottiOsumaa) {
								ottiOsumaaTimer += Time.deltaTime;
								if (ottiOsumaaTimer > 0.3f) {
										//Destroy (GameObject.FindWithTag ("verisuihku"));
										ottiOsumaa = false;
								}
						}

						transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
				}
		}

		void Flip ()
		{

				facingRight = !facingRight;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				
		}

		void OnTriggerEnter2D (Collider2D col)
		{
				if (col.gameObject.tag == "Saimaa") {
					saimaa = true;
				}

				if (col.gameObject.tag == "Forest") {
					forest = true;
				}

				if (col.gameObject.tag == "Ranta") {
					ranta = true;
				}
		}
	
		void OnTriggerExit2D (Collider2D col)
		{
			if (col.gameObject.tag == "Saimaa") {
				saimaa = false;
			}
			
			if (col.gameObject.tag == "Forest") {
				forest = false;
			}
			
			if (col.gameObject.tag == "Ranta") {
				ranta = false;
			}
		}

		void ApplyDamage (bool suunta)
		{
				hyokkaajanSuunta = suunta;
		}

		void ApplyDamage (int damage)
		{
				if (health > 0) {
						blockCounter--;
						if (blockCounter < 1 && nowBlocking) {
								anim.SetTrigger ("blockBroken");
								nowBlocking = false;
								hit.Play ();
						}

						if (nowBlocking && facingRight != hyokkaajanSuunta && blockCounter > 0) {

								gameObject.GetComponentInChildren<VeriLentaa> ().kipinoi ();
								if (blockCounter == 1) {
										gameObject.GetComponentInChildren<VeriLentaa> ().kipinoi ();
										clang.pitch = 1f;
										clang.volume = 1f; 
								}
					
								clang.Play ();
						} else {

								health -= damage;

								healthSlider.value = health;
								valahdys = true;
								hit.Play ();
								



								var number = Random.Range (5f, 50f);

								if (health > 0f) {
										gameObject.GetComponentInChildren<VeriLentaa> ().suihkauta ();
										ottiOsumaaTimer = 0f;
										ottiOsumaa = true;
								
									
										var pain = Random.Range (0f, 50f);
										
										if (pain < 11f) {
												pain1.Play ();
										} else if (pain < 20f) {
												pain2.Play ();
										} else if (pain < 30f) {
												pain3.Play ();
										} else {
												pain4.Play ();
										}
										

								} else if (!paaPoikki && number < 15f) {
										anim.SetBool ("chopOffHead", true);
										gameObject.GetComponentInChildren<MestausScript> ().katkaise ();
										paaPoikki = true;
										jalkaPoikki = true;
										anim.SetFloat ("Speed", 0);
										squirt.Play ();
										
								} else if (!jalkaPoikki && number >= 15f && number <= 24f) {
										anim.SetBool ("damageLeg", true);
										//gameObject.GetComponentInChildren<MestausScript> ().katkaise ();
										gameObject.GetComponentInChildren<RampautusScript> ().katkaise ();
										jalkaPoikki = true;
										paaPoikki = true;
										anim.SetFloat ("Speed", 0);
										squirt.Play ();
								} else {
										anim.SetTrigger ("heroFalls");
										gameObject.GetComponentInChildren<VeriLentaa> ().suihkauta ();
										//gameObject.GetComponentInChildren<MestausScript> ().katkaise ();
										//gameObject.GetComponentInChildren<RampautusScript> ().katkaise ();
										jalkaPoikki = true;
										paaPoikki = true;
										anim.SetFloat ("Speed", 0);
								}
								
								if (health < 1) {
										death.Play ();
								}
						}
				}
		}

		void InflictDamageToEnemy (GameObject enemy, int damage)
		{
				enemy.SendMessage ("ApplyDamage", 20);
		}

		void DecapitateEnemy (GameObject enemy)
		{
				enemy.SendMessage ("Decapitate");
		}

		void LegcapitateEnemy (GameObject enemy)
		{
				enemy.SendMessage ("Legcapitate");
		}

		bool IsOnSameVerticalLevelWithEnemy (GameObject enemy)
		{
				float yDifference = (enemy.transform.position.y - this.transform.position.y);
		
				if (Mathf.Abs (yDifference) > 0.2f) {
						return false;
				} else {
						return true;
				}
		}

		bool IsTooCloseToEnemy (GameObject enemy)
		{
				float xDifference = (enemy.transform.position.x - this.transform.position.x);
		
				if ((Mathf.Abs (xDifference) < 1.0f)) {
						return true;
				} else {
						return false;
				}
		}

		bool IsOnLegVerticalLevelWithEnemy (GameObject enemy)
		{
				float yDifference = (enemy.transform.position.y - this.transform.position.y);
		
				if ((Mathf.Abs (yDifference) < 0.2f) && (Mathf.Abs (yDifference) > 0.0f)) {
						return true;
				} else {
						return false;
				}
		}

		bool IsOnExactVerticalLevelWithEnemy (GameObject enemy)
		{
				float yDifference = (enemy.transform.position.y - this.transform.position.y);
		
				if (Mathf.Abs (yDifference) > 0.05f) {
						return false;
				} else {
						return true;
				}
		}

		public bool isHeroAlive ()
		{
				if (health <= 0) {
						return false;
				}
				return true;
		}

		void blockIsTrue ()
		{
				nowBlocking = true;
				blockCounter = Random.Range (3, 6);
		}

		public void IncreaseKillCount ()
		{
				this.killCount++;

				if (this.killCount == 10) {
						anim.SetTrigger ("victory");
						this.victory = true;
				}
		}

		public void nextScene ()
		{
			Application.LoadLevel ("Kartta");

		}

		public void BackToMainMenu ()
		{
				Application.LoadLevel ("MainMenu");
		}
}
