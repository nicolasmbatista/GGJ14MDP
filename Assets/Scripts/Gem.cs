using UnityEngine;
using System;
using System.Collections;

public class Gem : MonoBehaviour
{

		public Action<Gem> GemCollected;
		public LayerEnum GemColor;
		public GameObject Tutorial;
		public float TutorialDuration;
		public SpriteRenderer SpriteRendererToHideAfterCollectingGem;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnCollisionEnter2D (Collision2D coll)
		{
				if (coll.gameObject.tag == "Player") {
						if (GemCollected != null) {
								GemCollected (this);
								// show get gem animation?
								// show gem tutorial
								StartCoroutine ("ShowTutorial");
								// Metele crotadas papa!!
								SpriteRendererToHideAfterCollectingGem.enabled = false;
								collider2D.enabled = false;
								_AudioManager.Instance.PlaySound(_AudioManager.SoundEnum.GemCollection);
						}
				}
		}

		private IEnumerator ShowTutorial ()
		{
				Tutorial.SetActive (true);
				yield return new WaitForSeconds (TutorialDuration);
				Tutorial.SetActive (false);		
				this.gameObject.SetActive (false);
		}
}
