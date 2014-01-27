using UnityEngine;
using System.Collections;

public class HistoryNextButton : MonoBehaviour {

	public GameObject[] texts;
	private int currentNumText = 0;
	public string Value;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public virtual void OnClick(){
		if (currentNumText< texts.Length-1){
			texts[currentNumText].SetActive(false);
			currentNumText +=1;
			texts[currentNumText].SetActive(true);
		}
		else{
			Application.LoadLevel(Value);
		}
	}
	
}
