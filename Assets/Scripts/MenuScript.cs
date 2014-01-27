using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public bool isQuitButton = false;

	public bool isCreditsButton = false;

	public bool isBackButton = false;
	// Use this for initialization
	public Transform menuOption;
	void Start () {
	}

	void OnMouseEnter(){
		renderer.material.color = Color.yellow;		
	}

	void OnMouseExit(){
		renderer.material.color = Color.white;		
	}

	void OnMouseUp(){
		if (isQuitButton) {
						Application.Quit ();
				} else if (isCreditsButton) {
						Application.LoadLevel (2);
				} else if (isBackButton) {
						Application.LoadLevel(0);
				}
		else
		{
			Application.LoadLevel(1);
		}
	}

	// Update is called once per frame
	void Update () {
	

	}
}
