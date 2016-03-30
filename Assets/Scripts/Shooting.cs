using UnityEngine;
using System.Collections;
//using UnityEditor.Animations;
public class Shooting : MonoBehaviour {

	public int oben_offsetX;
	public int oben_offsetY;
	public int mitte_offsetX;
	public int mitte_offsetY;
	public int unten_offsetX;
	public int unten_offsetY;


	private Vector2 getDirection () {
		GameObject thePlayer = GameObject.Find("Player");
		BasicMovement movement = thePlayer.GetComponent<BasicMovement>();
		float lookY = Input.GetAxis ("Vertical");

		Vector2 dir = new Vector2(0, 0);
		// Wenn nach oben oder unten "geschaut" wird
		if (lookY != 0) {
			// Wenn nach oben geschaut wird
			if (lookY > 0) {
				dir.y = 1;
				// Wenn nach unten geschaut wird
			} else if (lookY < 0) {
				dir.y = -1;
			} else {
				if (movement.facingRight == true) {
					dir.x = 1;
				} else {
					dir.x = -1;
				}
			}
		} else {

			//Debug.Log ("Look:" + lookX);
			if (movement.facingRight == true) {
				dir.x = 1;

			} else {
				dir.x = -1;
			}
		}

		return dir;
	}

	// Use this for initialization
	void Start () {
		oben_offsetX = 0;
		oben_offsetY = 95;
		mitte_offsetX = 55;
		mitte_offsetY = 25;
		unten_offsetX = 6;
		unten_offsetY = 32;
	}
	// Finde die Richtung heraus, in die geschossen werden soll


	// Update is called once per frame
	void Update () {

	// Finde den Animator vom Gameobject "Koerper" und überprüfe ob die Firetaste gedrückt wird
	GameObject g = GameObject.Find ("Koerper");
	GameObject p = GameObject.Find ("Player");
	Animator b = g.GetComponent<Animator>();
	if( Input.GetButtonDown("Fire1")) {
		Debug.Log(p.GetComponent<Transform>().position);
		// Setze die bool "Shot" im Animator auf true und spiele den Schusssound ab
		b.SetBool ("Shot", true);
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();

		// Erstelle einen Shot und lasse ihn losfliegen
		Transform playerT = p.GetComponent<Transform>();
		Vector3 offsetpos = playerT.position;
		float lookY = Input.GetAxis ("Vertical");
		// Passe den Ausgangspunkt an

		// Hoch gucken
		if (lookY > 0) {
			//Bedingung ? true : false
			// Guckt man nach links? ja nein
			offsetpos.x = getDirection ().x == -1 ? offsetpos.x - oben_offsetX : offsetpos.x + oben_offsetX;
			offsetpos.y = getDirection ().y == -1 ? offsetpos.y - oben_offsetY : offsetpos.y + oben_offsetY;
			Debug.Log ("lookY > 0=" + lookY);
		}
		// Runter gucken
		else if (lookY < 0) {
			offsetpos.x = getDirection ().x == -1 ? offsetpos.x - unten_offsetX : offsetpos.x + unten_offsetX;
			offsetpos.y = getDirection ().y == -1 ? offsetpos.y - unten_offsetY : offsetpos.y - unten_offsetY;
			Debug.Log ("lookY < 0=" + lookY);
			// Geradeaus gucken
		} else {
			offsetpos.x = getDirection ().x == -1 ? offsetpos.x - mitte_offsetX : offsetpos.x + mitte_offsetX;
			offsetpos.y = getDirection ().y == -1 ? offsetpos.y - mitte_offsetY : offsetpos.y + mitte_offsetY;
			Debug.Log ("lookY = 0=" + lookY);
		}
		// Instanziiere einen Shot an der offset Position
		GameObject shotinstance = Instantiate (Resources.Load ("shot"), offsetpos, Quaternion.identity) as GameObject;
			shotinstance.GetComponent<Shot> ().direction = getDirection ();
		// Greife auf den Rigidbody des instanziierten Shots zu
		Rigidbody2D myRigidbody2D;
		myRigidbody2D = shotinstance.GetComponent<Rigidbody2D>();

		// Lasse den Shot in die richtige Richtung losfliegen und ignoriere die Kollision mit dem Spieler
		Vector2 currentvelo = myRigidbody2D.velocity;
		//currentvelo.x = Time.de
			myRigidbody2D.AddForce(getDirection());
		//myRigidbody2D.velocity.x = Time.deltaTime * 90000;

		//Physics2D.IgnoreCollision (shotinstance.GetComponent<Collider2D> (), p.GetComponent<Collider2D> ());
	}
	}

	void FixedUpdate () {
		// Setze die float "Y-Axis" im Animator vom GameObject Koerper auf den Wert der vertikalen Eingabeachse
		float look = Input.GetAxis ("Vertical");
		GameObject g = GameObject.Find ("Koerper");
		Animator b = g.GetComponent<Animator> ();
		b.SetFloat ("Y-Axis", (look));
}

	void LateUpdate () {
		// Setze die bool "Shot" im Animator wieder auf false
		// Das ist sehr wahrscheinlich nicht der schönste Weg einen Shot zu togglen
		GameObject g = GameObject.Find ("Koerper");
		Animator b = g.GetComponent<Animator> ();
		b.SetBool ("Shot", false);
}
	}