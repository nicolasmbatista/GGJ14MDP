using UnityEngine;
using System.Collections;

public class SpawnerObject : MonoBehaviour
{
		private bool _alreadySpawned = false;

		public float timeUntillSpawn;

		public GameObject itemToSpawn;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collider2D c)
		{
				if (!_alreadySpawned) {
						StartCoroutine ("Spawn");
						_alreadySpawned = true;
				}
		} 

		IEnumerator Spawn ()
		{
				yield return new WaitForSeconds (timeUntillSpawn);
				Transform transformSpawn = transform.GetChild (0) as Transform;
				itemToSpawn.SetActive (true);
				itemToSpawn.transform.position = new Vector3 (transformSpawn.position.x, 0, 0);
		}
}
