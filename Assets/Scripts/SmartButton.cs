using UnityEngine;
using System.Collections;

public class SmartButton : MonoBehaviour {
	
	public bool UseAsURL;
	public bool UseAsSceneLoader;
	public bool UseAsPanelSwitcher;
	public bool UseAsIAPPurchase;
	public bool UseAsExit;
	public string Value;
	public GameObject GoToPanel;
	public GameObject OurParentPanel;

	
	public static System.Action<SmartButton> OnSmartButtonClickEvent;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		OnClick();
	}
	
	void OnMouseEnter(){
		if(renderer != null)
			renderer.material.color = Color.yellow;		
	}
	
	void OnMouseExit(){
		if(renderer != null)
			renderer.material.color = Color.white;		
	}

	public virtual void OnClick()
	{
		if(UseAsURL)
		{
			Application.OpenURL(Value);
		}
		else if (UseAsSceneLoader)
		{
			Application.LoadLevel(Value);
		}
		else if (UseAsPanelSwitcher)
		{
			OurParentPanel.SetActive(false);
			GoToPanel.SetActive(true);
		}
		else if (UseAsIAPPurchase)
		{
			//_IAPManager.Purchase(Value);
		}
		else if (UseAsExit)
		{
			Application.Quit();
		}
		else
		{
			if(OnSmartButtonClickEvent != null)
				OnSmartButtonClickEvent(this);
		}
	}
}
