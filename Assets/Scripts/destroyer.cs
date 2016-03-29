using UnityEngine;
using System.Collections;

public class destroyer : MonoBehaviour {

	public GameObject parent;


	public void destroy() {
		Debug.Log ("Hallo");
		Destroy (this.transform.parent.gameObject);
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		parent = this.transform.parent.gameObject;
		if (parent.transform.position.x >= 4000 || parent.transform.position.x <= -500 || parent.transform.position.y <= -100 || parent.transform.position.y >= 200) {
			Destroy (parent);
		}
		//if this.transform.parent.gameObject.transform.x >
	}
}
