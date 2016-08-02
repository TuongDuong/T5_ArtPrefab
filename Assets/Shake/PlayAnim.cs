using UnityEngine;
using System.Collections;

public class PlayAnim : MonoBehaviour {
	public float delayTime;
	public float attackTime;
	private tk2dSpriteAnimator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<tk2dSpriteAnimator> ();
		if (anim != null) {
			InvokeRepeating ("PlayAnimation", delayTime, attackTime);
		}
	}

	void PlayAnimation() {
		anim.Play ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
