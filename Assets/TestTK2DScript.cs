using UnityEngine;
using System.Collections;

public class TestTK2DScript : MonoBehaviour {
	public tk2dSpriteAnimator[] tk2dAnim;

	// Use this for initialization
	void Start () {
		foreach (tk2dSpriteAnimator anim in tk2dAnim)
		{
			anim.Play();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
