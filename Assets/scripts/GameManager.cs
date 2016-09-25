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
	private bool gameOver;

	private void Start () {
		Time.timeScale = 1;

		player.onHitEnemy += onGameOver;
		player.onKillEnemy += onKillEnemy;
		enemyCount = enemyGroup.GetComponentsInChildren<Enemy> ().Length;
	}
}
