using UnityEngine;
using System;
using System.Collections;

public class Gate : MonoBehaviour
{

		public Action<Gate> OnGateActivated;
		public bool isActivated;
		public GameObject Particles;

		private int _id;

		// Use this for initialization
		void Start ()
		{
				this.isActivated = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void setId (int id)
		{
				this._id = id;
		}

		public int getId ()
		{
				return _id;
		}

		void OnCollisionEnter2D (Collision2D coll)
		{
				if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Box") {
						if (OnGateActivated != null) {
								OnGateActivated (this);
								Particles.SetActive (true);
						}
				}
		}

		public void DeactivateGate ()
		{
				Particles.SetActive (false);
				this.isActivated = false;
		}

}
