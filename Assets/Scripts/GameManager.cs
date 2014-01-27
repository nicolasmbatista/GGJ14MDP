using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
		public List<GameObject> ObjectsInRed;
		public List<GameObject> ObjectsInGreen;
		public List<GameObject> ObjectsInBlue;
		public Gem[] Gems;
		public ParticleSystem ChangeToRed;
		public ParticleSystem ChangeToGreen;
		public ParticleSystem ChangeToBlue;
		public bool CheatsOn;

		// red green blue
		private List<LayerEnum> activeColors;
		private Dictionary<LayerEnum, List<GameObject>> LayerToObjectsMap = new Dictionary<LayerEnum, List<GameObject>> ();
		private Dictionary<LayerEnum, ParticleSystem> LayerToEffectsMap = new Dictionary<LayerEnum, ParticleSystem> ();
	
		private LayerEnum ActiveLayer = LayerEnum.DEFAULT;

		public GameObject character;
		public Transform[] BackgroundEnviroments;

		private List<List<Transform>> bgLists;

		public void Awake ()
		{
				base.Awake ();
				foreach (Gem gem in Gems) {
						gem.GemCollected += GemCollected;
				}		
				activeColors = new List<LayerEnum> ();
		}

		private void GemCollected (Gem gem)
		{
				activeColors.Add (gem.GemColor);

		}

		public bool IsColorActive (LayerEnum layer)
		{
				foreach (LayerEnum color in activeColors) {
						if (color == layer) {
								return true;
						}
				}
				return false;
		}

		public void ChangeToLayer (LayerEnum layer)
		{
				if (layer == ActiveLayer)
						return;
				bool isColorActive = false;

				foreach (LayerEnum color in activeColors) {
						if (color == layer) {
								isColorActive = true;
						}
				}

				if (!isColorActive) {
						return;
				}

				SpawnParticleSystem (ActiveLayer);
				Hide (LayerEnum.RED);
				Hide (LayerEnum.GREEN);
				Hide (LayerEnum.BLUE);
				Show (layer);
				ActiveLayer = layer;
		}

		// Turn on the bit using an OR operation:
		private void Show (LayerEnum layer)
		{
				List<GameObject> list = LayerToObjectsMap [layer];
				if (list == null) {
						return;
				}
				foreach (GameObject go in list) {
						go.SetActive (true);
				}
		}
	
		// Turn off the bit using an AND operation with the complement of the shifted int:
		private void Hide (LayerEnum layer)
		{
				List<GameObject> list = LayerToObjectsMap [layer];
				if (list == null) {
						return;
				}
				foreach (GameObject go in list) {
						go.SetActive (false);
				}
		}

		// Use this for initialization
		void Start ()
		{
				// Populate structures
				ObjectsInRed = GetElementsInLayer (LayerEnum.RED);
				ObjectsInGreen = GetElementsInLayer (LayerEnum.GREEN);
				ObjectsInBlue = GetElementsInLayer (LayerEnum.BLUE);

				LayerToObjectsMap.Add (LayerEnum.RED, ObjectsInRed);
				LayerToObjectsMap.Add (LayerEnum.GREEN, ObjectsInGreen);
				LayerToObjectsMap.Add (LayerEnum.BLUE, ObjectsInBlue);
				LayerToObjectsMap.Add (LayerEnum.GROUND, null);
				LayerToObjectsMap.Add (LayerEnum.DEFAULT, null);
		
				LayerToEffectsMap.Add (LayerEnum.RED, ChangeToRed);
				LayerToEffectsMap.Add (LayerEnum.GREEN, ChangeToGreen);
				LayerToEffectsMap.Add (LayerEnum.BLUE, ChangeToBlue);
				LayerToEffectsMap.Add (LayerEnum.GROUND, null);
				LayerToEffectsMap.Add (LayerEnum.DEFAULT, null);


				bgLists = new List<List<Transform>> ();
			
				foreach (Transform background in BackgroundEnviroments) {
						List<Transform> frameList = new List<Transform> ();
						for (var i = 0; i < background.childCount; i++) {
								frameList.Add (background.GetChild (i));

						}
						bgLists.Add (frameList);
				}
				Hide (LayerEnum.RED);
				Hide (LayerEnum.GREEN);
				Hide (LayerEnum.BLUE);
		}
	
		// Update is called once per frame
		void Update ()
		{
				// Follow the main character
				this.transform.position = new Vector3 (character.transform.position.x, this.transform.position.y, this.transform.position.z);
		}

		public void ReorderBackgrounds (bool goingRight)
		{
				foreach (List<Transform> bgList in bgLists) {
						//when the first BG on the list exit the barrier trigger, this snippet is called
						Transform firstBG = bgList.FirstOrDefault ();
						Transform lastBG = bgList.LastOrDefault ();
						if (goingRight) {
								firstBG.position = new Vector3 (lastBG.transform.position.x + lastBG.renderer.bounds.size.x, 0, firstBG.transform.position.z);
		
								bgList.Remove (firstBG);
								bgList.Add (firstBG);
						} else {
								lastBG.position = new Vector3 (firstBG.transform.position.x - firstBG.renderer.bounds.size.x, 0, lastBG.transform.position.z);

								bgList.Remove (lastBG);
								bgList.Insert (0, lastBG);
						}
				}
		}

		private void SpawnParticleSystem (LayerEnum layer)
		{
				List<GameObject> ObjectsToReplace = LayerToObjectsMap [layer];
				ParticleSystem ps = LayerToEffectsMap [layer];

				if (ObjectsToReplace == null || ps == null)
						return;

				foreach (GameObject obj in ObjectsToReplace) {
						Instantiate (ps, obj.transform.position, Quaternion.identity);
				}
		}

		/**
		 * WARNING: Use sparingly as it is slow.
		 */
		private List<GameObject> GetElementsInLayer (LayerEnum layer)
		{
				GameObject[] goArray = FindObjectsOfType (typeof(GameObject)) as GameObject[];
				int targetLayer = (int)layer;		

				List<GameObject> goList = goArray.Where (obj => ((obj.layer == targetLayer) && (obj.tag != "Barrier_Right"))).ToList (); 

				return goList;
		
		}

		void OnGUI ()
		{
				if (this.CheatsOn) {
						if (GUILayout.Button ("RED")) {
								Gem gem = new Gem ();
								gem.GemColor = LayerEnum.RED;
								GemCollected (gem);
						}
						if (GUILayout.Button ("GREEN")) {
								Gem gem = new Gem ();
								gem.GemColor = LayerEnum.GREEN;
								GemCollected (gem);
						}
						if (GUILayout.Button ("BLUE")) {
								Gem gem = new Gem ();
								gem.GemColor = LayerEnum.BLUE;
								GemCollected (gem);
						}
				}
		}

}
