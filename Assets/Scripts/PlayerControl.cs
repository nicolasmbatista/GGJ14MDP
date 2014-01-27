using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
		[HideInInspector]
		public bool
				facingRight = true;			// For determining which way the player is currently facing.
		[HideInInspector]
		public bool
				jump = false;				// Condition for whether the player should jump.


		public float moveForce = 365f;			// Amount of force added to move the player left and right.
		public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
		public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
		public float jumpForce = 1000f;			// Amount of force added when the player jumps.
		//public AudioClip[] taunts;				// Array of clips for when the player taunts.
		//public float tauntProbability = 50f;	// Chance of a taunt happening.
		//public float tauntDelay = 1f;			// Delay for when the taunt should happen.


		//private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
		//private Transform groundCheck;			// A position marking where to check if the player is grounded.
		public bool grounded = false;			// Whether or not the player is grounded.
		private Animator anim;					// Reference to the player's animator component.
		public bool againstWall = false;
		private bool canBeGrounded = true;
		public float JumpDelay = 0.2f;

		private GameManager _gm;
		private bool _againstWall = false;
	

		void Awake ()
		{
				// Setting up references.
				//groundCheck = transform.Find ("groundCheck");
				anim = GetComponent<Animator> ();
		}


		void Update ()
		{
				if (Input.GetKey (KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1)) {
						if (GameManager.Instance.IsColorActive (LayerEnum.RED)) {
								anim.SetInteger ("Color", 0);
						}
						ChangeToLayer (LayerEnum.RED);	
				}
				if (Input.GetKey (KeyCode.Keypad2)|| Input.GetKey(KeyCode.Alpha2)) {
						if (GameManager.Instance.IsColorActive (LayerEnum.GREEN)) {
								anim.SetInteger ("Color", 1);
						}
						ChangeToLayer (LayerEnum.GREEN);
				}
				if (Input.GetKey (KeyCode.Keypad3)|| Input.GetKey(KeyCode.Alpha3)) {
						if (GameManager.Instance.IsColorActive (LayerEnum.BLUE)) {
								anim.SetInteger ("Color", 2);
						}
						ChangeToLayer (LayerEnum.BLUE);
				}
				// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
				//grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));  

				// If the jump button is pressed and the player is grounded then the player should jump.
				if (Input.GetButtonDown ("Jump") && grounded) {
						jump = true;
						grounded = false;
						StartCoroutine ("StartJumpDelay");
				}
		}

		private IEnumerator StartJumpDelay ()
		{
				canBeGrounded = false;
				yield return new WaitForSeconds (JumpDelay);
				canBeGrounded = true;
		}

		void FixedUpdate ()
		{
				// Cache the horizontal input.
				float h = Input.GetAxis ("Horizontal");

				if (againstWall) {
						h = 0;
				}

				// The Speed animator parameter is set to the absolute value of the horizontal input.
				anim.SetFloat ("Speed", Mathf.Abs (h));
				if (h == 0.0f) {
						anim.SetBool ("Idle", true);
				} else {
						anim.SetBool ("Idle", false);
				}

				// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
				if (h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
						rigidbody2D.AddForce (Vector2.right * h * moveForce);

				// If the player's horizontal velocity is greater than the maxSpeed...
				if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
						rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

				// If the input is moving the player right and the player is facing left...
				if (h > 0 && !facingRight)
			// ... flip the player.
						Flip ();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight)
			// ... flip the player.
						Flip ();

				// If the player should jump...
				if (jump) {
						// Set the Jump animator trigger parameter.
//			anim.SetTrigger("Jump");

						// Play a random jump audio clip.
						//int i = Random.Range (0, jumpClips.Length);
						//AudioSource.PlayClipAtPoint (jumpClips [i], transform.position);

						// Add a vertical force to the player.
						rigidbody2D.AddForce (new Vector2 (0f, jumpForce));

						// Make sure the player can't jump again until the jump conditions from Update are satisfied.
						jump = false;
				}
		}
	
	
		public void Flip ()
		{
				// Switch the way the player is labelled as facing.
				facingRight = !facingRight;

				// Multiply the player's x local scale by -1.
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
		}

		// Use this for initialization
		void Start ()
		{
				_gm = GameManager.Instance;
				_gm.ChangeToLayer (LayerEnum.GROUND);
				anim.SetInteger ("Color", -1);
		}

	
		private void ChangeToLayer (LayerEnum layer)
		{
				this.gameObject.layer = (int)layer;
				_gm.ChangeToLayer (layer);
				grounded = false;
		}
	
		void OnCollisionEnter2D (Collision2D coll)
		{
				string tag = coll.gameObject.tag;
				if (tag == "Wall") {
						againstWall = true;
				} else if (tag == "ground") {
						if (canBeGrounded)
								grounded = true;
				}
		}
	
		void OnCollisionStay2D (Collision2D coll)
		{
				string tag = coll.gameObject.tag;
				if (againstWall && rigidbody2D.velocity.y == 0) {
						againstWall = false;
				} else if (tag == "ground") {
						if (canBeGrounded)
								grounded = true;
				}
		}
	
		void OnCollisionExit2D (Collision2D coll)
		{
				string tag = coll.gameObject.tag;
				if (tag == "Wall") {
						againstWall = false;
				} else if (tag == "ground") {
						grounded = false;
				}
		
		}
	
	
	
}
