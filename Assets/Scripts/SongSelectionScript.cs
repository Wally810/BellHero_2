using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SongSelectionScript : MonoBehaviour
{
   public static string ClickedButtonName = "";
    public void loadStartScreen(){
        SceneManager.LoadScene("StartScreen");
    }

    public void clickedButton(){
        ClickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(ClickedButtonName + " has been clicked");
        SceneManager.LoadScene("GameScreen");
    }
}