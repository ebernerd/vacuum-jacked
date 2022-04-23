using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
	[SerializeField] AudioSource introMusic;

	public void PlayGame()
	{
		introMusic.loop = false;
	}

	void Update()
	{
		if (!introMusic.isPlaying)
		{
			SceneManager.LoadScene("Scenes/FightScene");
		}
	}

	public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
