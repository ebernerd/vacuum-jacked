using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public enum GameState {
		Fighting,
		GameOver
	}

	public static GameManager GetGameManager() {
		return GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
	}

	private GameState gameState = GameState.Fighting;
	public GameState GetGameState() {
		return gameState;
	}

	public void EndGame() {
		SetGameState(GameState.GameOver);
	}

	public void SetGameState(GameState newGameState) {
		gameState = newGameState;
		//	Any updates / code here
		switch (newGameState) {
			case GameState.GameOver: {
				SceneManager.LoadScene("Scenes/GameOverShell");
				break;
			}
			default: break;
		}
	}
}
