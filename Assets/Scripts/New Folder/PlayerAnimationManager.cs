using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {


	private Animator animator;
	private InputState inputState;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		inputState = GetComponent<InputState> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		var running = false;

		if (inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold)
			running = true;
		animator.SetBool ("Running", running);
		Debug.Log ("Test");
	}
}
