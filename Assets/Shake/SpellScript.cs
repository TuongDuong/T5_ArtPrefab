using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour {
	
	public GameObject spellPrefab;
	public Camera mainCamera;
	public Transform BackgroundPos;
	public bool random = false;
	public GameObject[] randomSpellPrefab;
	
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			// Vector3 mousePos1 = new Vector3(mousePos.x, mainCamera.transform.position.y - 200f, mousePos.z);
						Vector3 mousePos1 = new Vector3(mousePos.x, mousePos.y, (mainCamera.transform.position.z + BackgroundPos.position.z)/2);
			
			int numRandom = UnityEngine.Random.Range(0, randomSpellPrefab.Length);
			if (random)	{
				Instantiate(randomSpellPrefab[numRandom], mousePos1, spellPrefab.transform.rotation);
			} else {
				Instantiate(spellPrefab, mousePos1, spellPrefab.transform.rotation);
			}
		}
	}
}