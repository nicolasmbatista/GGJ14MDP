using UnityEngine;
using System.Collections;

public class BarrierScript : MonoBehaviour
{
		public bool GoingRight = true;
		
		private GameManager _gm;
		private PlayerControl _playerControler;

		// Use this for initialization
		void Start ()
		{
				_gm = GameManager.Instance;
				_playerControler = _gm.character.GetComponent<PlayerControl> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collider2D c)
		{
				if (_playerControler.facingRight == GoingRight && c.tag == "Player")
						_gm.ReorderBackgrounds (GoingRight);
		}
}
