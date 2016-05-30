using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {
	public Animator thisAnimator;

	void Awake() {
		thisAnimator = this.gameObject.GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			thisAnimator.SetTrigger("SexNow");
		}
	}
}
