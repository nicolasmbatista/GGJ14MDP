using UnityEngine;
using System.Collections;

public class _AudioManager : Singleton<_AudioManager>
{

		public AudioSource audioSource;
		public AudioSource ambience;
		public AudioClip[] clips;
		public AudioClip[] AmbienceClips;

		public enum SoundEnum
		{
				GemCollection = 0
		}

		// Use this for initialization
		void Start ()
		{
				StartCoroutine ("PlayAmbience");
		}

		public IEnumerator PlayAmbience ()
		{
				while (true) {
						int clip = (int)Random.Range (0, AmbienceClips.Length);
						ambience.clip = AmbienceClips [clip];
						ambience.Play ();
						float secondsToWait = AmbienceClips [clip].length + Random.Range (0, 10);
						yield return new WaitForSeconds (secondsToWait);
				}
		}

		// Update is called once per frame
		void Update ()
		{
	
		}

		public void PlaySound (SoundEnum sound)
		{
				/*audioSource.clip = clips[(int)sound];
		audioSource.Play();*/
		}
}
