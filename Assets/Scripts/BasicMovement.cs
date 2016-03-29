using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

	//Definiere Variablen und Listen

	//Movement related
	private float currentSpeed = 140f;
	public float runSpeed = 140f;
	public float sprintSpeed = 200f;
	public bool facingRight = true;

	Animator[] anims;
	Component[] comps;

	//Jump related
	public Transform groundCheck;
	float groundRadius = 10f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	// Use this for initialization
	Rigidbody2D myRigidbody2D;

	void Start () {
		
		myRigidbody2D = this.GetComponent<Rigidbody2D>();
		anims = GetComponentsInChildren<Animator> ();
	}

	public void setMaxSpeed (float value) {
		this.currentSpeed = value;
	}

	void FixedUpdate () {
		// Überprüfe ob der Spieler "grounded" ist
		GameObject g = GameObject.Find ("Beine");
		bool grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		// Setzte die bool "Ground" im Animator vom GameObject "Beine" auf grounded
		Animator b = g.GetComponent<Animator> ();
		b.SetBool ("Ground", grounded);

		float move = Input.GetAxis ("Horizontal");

		foreach (Animator iter in anims) {
			if (currentSpeed == runSpeed) {
				iter.SetFloat ("Speed", Mathf.Abs (move));

			}
			else iter.SetFloat ("Speed", Mathf.Abs (move)*2);
		}

		myRigidbody2D.velocity = new Vector2 (move * currentSpeed, myRigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	// Update is called once per frame
	void Update () {
		bool grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		//Starte Shift-Sprint
		if(grounded && Input.GetKeyDown(KeyCode.LeftShift)) {
			//Debug.Log ("Hallo");
			BasicMovement sprint = GetComponent<BasicMovement> ();
			sprint.setMaxSpeed (sprint.sprintSpeed);

			float testo = Input.GetAxis ("Vertical");
			Debug.Log ("Vert Axis" + testo);

		}
		//Beende Shift-Sprint
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			BasicMovement sprint = GetComponent<BasicMovement> ();
			sprint.setMaxSpeed (sprint.runSpeed);
		}
		//Boden Collision-Check

		GameObject g = GameObject.Find ("Beine");
		Animator b = g.GetComponent<Animator> ();
		if(grounded && Input.GetButtonDown("Jump")) {

			b.SetBool ("Ground", false);
			myRigidbody2D.AddForce(new Vector2(100f, jumpForce));
		}
	}
}
