using UnityEngine;
using System.Collections;

public class MoveUnit : MonoBehaviour {
	public Transform target;
	public float time;
	public float delayTime;


	// Use this for initialization
	void Start () {
		Invoke ("PlayAnim", delayTime);
	}
	
	void PlayAnim() {
		if (target != null) {
			LeanTween.move (gameObject, target, time);
		}
	} 
	
	// Update is called once per frame
	void Update () {		
	}
}
