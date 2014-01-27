using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		originalPos = door.localPosition;
	}
	private Vector3 originalPos;
	public Transform door;
	// Update is called once per frame
	void Update () {
	
		if (isDoorOpen) {
						if (door.localPosition.z >= -7.8f)
								door.Translate (new Vector3 (0, 0, 0.3f));
						else
								Reset ();
				} else {
						if(isReseting)
						{
							if (door.localPosition.z < originalPos.z)
								door.Translate (new Vector3 (0, 0, -0.1f));
						}
				}
	}

	private bool isDoorOpen = true;
	private bool isReseting = false;
	public void Open()
	{
		isDoorOpen = true;
	}

	public void Reset()
	{
		isDoorOpen = false;
		isReseting = true;
		//door.localPosition = originalPos;
	}


}
