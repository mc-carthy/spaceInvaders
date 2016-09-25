using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	private Camera mainCam;
	[SerializeField]
	private Text scoreText, gameoverText;
	[SerializeField]
	private Player player;
	[SerializeField]
	private EnemyGroup enemyGroup;
	private int score;
	private int enemyCount;
	private float gameTimer;
	private bool gameover;

	private void Start () {
		Time.timeScale = 1;

		player.onHitEnemy += OnGameOver;
		player.onKillEnemy += OnKillEnemy;
		enemyCount = enemyGroup.GetComponentsInChildren<Enemy> ().Length;
	}

	private void Update () {
		if (gameover) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);
			}
			return;
		}
		scoreText.text = "Score: " + score;
	}

	private void OnKillEnemy () {
		score += 100;
		enemyCount--;
		if (enemyCount <= 0) {
			OnGameOver ();
		}
	}

	private void OnGameOver () {
		gameover = true;
		scoreText.enabled = false;
		gameoverText.enabled = true;

		if (enemyCount != 0) {
			gameoverText.text = "Game Over!\nScore: " + score + "\nPress R to Restart";
		} else {
			gameoverText.text = "You Win!\nScore: " + score + "\nPress R to Restart";
		}

		Time.timeScale = 0;
	}
}
