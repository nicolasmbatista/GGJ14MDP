using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour
{
		public bool CanMove = false;
		public float LinearDrag = 1f;
		public bool FixedAngle = true;
		public float GravityScale = 2f;

		private Rigidbody2D _rb;

		public void Awake ()
		{
				_rb = this.rigidbody2D;
				// Condiciones de fisica necesarias para que los objetos se muevan adecuadamente
				if (_rb) {
						if (CanMove) {
								_rb.drag = LinearDrag;
								_rb.fixedAngle = FixedAngle;
								_rb.gravityScale = GravityScale;
						} else {
								_rb.isKinematic = true;
						}
				}
		}

		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
