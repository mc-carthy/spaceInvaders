using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour {

	[SerializeField]
	private float speed = 10;
	private Action _onKillEnemy;
	public Action onKillEnemy { set { this._onKillEnemy = value; } }

	private void Update () {
		transform.position = new Vector3 (
			transform.position.x, 
			transform.position.y + speed * Time.deltaTime,
			transform.position.z
		);

		if (transform.position.y > 6) {
			Destroy (gameObject);
		}
	}

	private void OnTriggerEnter2D (Collider2D trig) {
		if (trig.gameObject.GetComponent<Enemy> () != null) {
			_onKillEnemy ();
			Destroy (trig.gameObject);
			Destroy (gameObject);
		}
	}
}
