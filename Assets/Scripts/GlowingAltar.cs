using UnityEngine;
using System.Collections;

public class GlowingAltar : MonoBehaviour {

	// Use this for initialization
	public Light altarLight;
	void Start () {
	
	}
	
	// Update is called once per frame
	private bool goingUp = true;
	void Update () {
	

		if (goingUp) {

						if (altarLight.intensity >= 2.5f)
								goingUp = false;
						else
								altarLight.intensity += 0.025f;
				} else {

						if (altarLight.intensity <= 1.0f)
							goingUp = true;
						else
							altarLight.intensity -= 0.025f;
				}

	}
}
