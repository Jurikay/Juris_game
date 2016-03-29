using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	AnimatorStateInfo animatorInfo;
	Rigidbody2D myRigidbody2D;

	public Vector2 direction;
	private float elapsedT;

	void Start () {
		elapsedT = 0f;

	}

	void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		Animator a = this.GetComponentInChildren<Animator> ();
		a.SetBool ("Collision", true);
		print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
		print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
		print("Their relative velocity is " + collisionInfo.relativeVelocity);
	}

	void OnCollisionStay2D(Collision2D collisionInfo)
	{
		
		
	}

	void OnCollisionExit2D(Collision2D collisionInfo)
	{
		print(gameObject.name + " and " + collisionInfo.collider.name + " are no longer colliding");
	}

	void Update () {
		elapsedT += Time.deltaTime;
		myRigidbody2D = this.GetComponent<Rigidbody2D> ();
		Vector2 newvelo = myRigidbody2D.velocity;
		if (direction == Vector2.right) {
			newvelo.x = elapsedT * 4200;
		} else if (direction == Vector2.left) {
			newvelo.x = elapsedT * -3300;
		} else if (direction == Vector2.up) {
			newvelo.y = elapsedT * 3300;
		} else if (direction == Vector2.down) {
			newvelo.y = elapsedT * -3300;
		}
		if (newvelo.x > 1500) {
			newvelo.x = 1500;
		}
		if (newvelo.y > 1500) {
			newvelo.y = 1500;
		}
		myRigidbody2D.velocity = newvelo;

		Animator a = this.GetComponentInChildren<Animator> ();
		animatorInfo = a.GetCurrentAnimatorStateInfo(0);
		Debug.Log ("AnimatorInfo:"+ animatorInfo);
//		if (animatorInfo.IsName ("destroy")) {
//			Debug.Log ("Hallo");
//			Destroy (this.gameObject);
//		}
	}
}