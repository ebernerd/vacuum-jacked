using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverShellUI : MonoBehaviour
{
	public void PlayAgain() {
		SceneManager.LoadScene("Scenes/FightScene");
	}

	public void Quit() {
		Debug.Log("QUIT");
		Application.Quit();
	}
}
