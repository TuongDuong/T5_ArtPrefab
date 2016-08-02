using UnityEngine;
using System.Collections;

public class MoveUnitPrince : MonoBehaviour {
	public Transform target;
	public float time;
	public float delayTime;
	public float delayChargeTime;
//	public string moveClip;
	public float chargeTime;
	public string chargeClip;
	public string shieldClip;
	public tk2dSpriteAnimator animator;
	public tk2dSpriteAnimator shieldAnimator;

	// Use this for initialization
	void Start () {
		Invoke ("PlayAnim", delayTime);
	}
	
	void PlayAnim() {
		if (target != null) {
			LeanTween.move (gameObject, target, time);
			Invoke ("PlayClip", delayChargeTime);
			Invoke ("PlayShieldClip", delayChargeTime);
		}
	}

	void PlayClip() {
		if (animator != null) {
			LeanTween.cancel( gameObject );
			LeanTween.move (gameObject, target, chargeTime);
			animator.Play(chargeClip);
		}
	}

	void PlayShieldClip() {
		if (shieldAnimator != null && shieldClip != null) {
//			LeanTween.cancel( gameObject );
//			LeanTween.move (gameObject, target, chargeTime);
			shieldAnimator.Play(shieldClip);
		}
	}
	
	// Update is called once per frame
	void Update () {		
	}
}
