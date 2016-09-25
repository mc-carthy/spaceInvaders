using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public delegate void onHitEnemyAction ();
	public delegate void onKillEnemyAction ();
	public onHitEnemyAction onHitEnemy;
	public onKillEnemyAction onKillEnemy;

	private float xClamp = 5.5f;
	private float speed = 6;
	private float shootCooldown = 0.5f;

	[SerializeField]
	private Bullet bullet;
	private float shootTimer = 0;

	private void Update () {
		shootTimer -= Time.deltaTime;
		GetInput ();
	}

	private void OnTriggerEnter2D (Collider2D trig) {
		if (trig.tag == "enemy") {
			if (onHitEnemy != null) {
				onHitEnemy ();
			}
		}
	}

	private void GetInput () {
		float xMove = Input.GetAxisRaw ("Horizontal");
		Vector2 temp = transform.position;
		temp.x += xMove * speed * Time.deltaTime;
		temp.x = Mathf.Clamp (transform.position.x, -xClamp, xClamp);
		transform.position = temp;

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (shootTimer <= 0) {
				shootTimer = shootCooldown;
				Shoot ();
			}
		}
	}

	private void Shoot () {
		Bullet newBullet = Instantiate (
			bullet,
			transform.position,
			Quaternion.identity
		) as Bullet;
		newBullet.transform.SetParent (transform);
		newBullet.GetComponent<Bullet> ().onKillEnemy = () => {
			if (this.onKillEnemy != null) {
				this.onKillEnemy ();
			}
		};
	}
}
