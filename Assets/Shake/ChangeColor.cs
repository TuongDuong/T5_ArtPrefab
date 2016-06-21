using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeColor : MonoBehaviour {

  public Material mat;

  void LateUpdate() {
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material = mat;
  }
}
