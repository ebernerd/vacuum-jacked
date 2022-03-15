using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour{
 
   public void PlayGame(){
       SceneManager.LoadScene("Scenes/FightScene");
   }     
    
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
