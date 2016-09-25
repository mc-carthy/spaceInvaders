using UnityEngine;
using System.Collections;

public class EnemyGroup : MonoBehaviour {

	private float xSpeed = 0.25f, ySpeed = 0.25f;
	private float maxInterval = 0.3f, minInterval = 0.025f;
	private float xClamp = 8f;

	private Enemy[] enemies;
	private float moveTimer;
	private int totalEnemies;
	private bool isMovingRight;

	private void Start () {
		enemies = GetComponentsInChildren<Enemy> ();
		totalEnemies = enemies.Length;
	}

	private void Update () {
		if (Time.timeScale != 0) {
			moveTimer -= Time.deltaTime;
			if (moveTimer <= 0) {
				enemies = GetComponentsInChildren<Enemy> ();
				int enemyCount = enemies.Length;
				float difficultyPercentage = 1 - ((float)enemyCount / totalEnemies);
				float interval = maxInterval + (minInterval - maxInterval) * difficultyPercentage;
				moveTimer = interval;

				float minX = 0;
				float maxX = 0;

				foreach (Enemy enemy in enemies) {
					if (enemy.transform.position.x < minX) {
						minX = enemy.transform.position.x;
					} else if (enemy.transform.position.x > maxX) {
						maxX = enemy.transform.position.x;
					}
				}

				if ((isMovingRight && maxX >= xClamp) || (!isMovingRight && minX < -xClamp)) {
					transform.position = new Vector3 (
						transform.position.x,
						transform.position.y - ySpeed,
						transform.position.z
					);
					isMovingRight = !isMovingRight;
				} else {
					transform.position = new Vector3 (
						transform.position.x + xSpeed * (isMovingRight ? 1 : -1),
						transform.position.y,
						transform.position.z
					);
				}
			}
		}
	}
}
