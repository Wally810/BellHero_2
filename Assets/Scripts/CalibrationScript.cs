using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalibrationScript : MonoBehaviour
{
    public void loadOptions(){
        SceneManager.LoadScene("OptionsScreen");
    }
}

