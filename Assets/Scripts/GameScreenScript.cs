using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreenScript : MonoBehaviour
{
    //Method to load the SongSelectionScreen when the attached button is clicked
    public void loadSongSelectionScreen(){
        SceneManager.LoadScene("SongSelectionScreen");
    }
}
