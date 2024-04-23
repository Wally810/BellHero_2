using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreenScript : MonoBehaviour
{
    public void loadSongSelection(){
        SceneManager.LoadScene("SongSelectionScreen");
    }

     public void loadCalibration(){
        SceneManager.LoadScene("CalibrationScreen");
    }

    public void loadOptions(){
        SceneManager.LoadScene("OptionsScreen");
    }
    
}
