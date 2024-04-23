using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScreenScript : MonoBehaviour
{

    //Method to load the StartScreen when the attached button is clicked
    public void loadStartScreen(){
        SceneManager.LoadScene("StartScreen");
    }

    //Method to load the CalibrationScreen when the attached button is clicked
    public void loadCalibration(){
        SceneManager.LoadScene("CalibrationScreen");
    }

    //Variables to get the sliderValue for sensitivity
    GameObject sliderObject;
    public Slider sensitivitySlider;
    public static float sensitivityValue;

    void Start()
    {
        //On start the objects are assigned their references
        GameObject sliderObject = GameObject.Find("SensitivitySlider");
        sensitivitySlider = sliderObject.GetComponent<Slider>();
        sensitivityValue = sensitivitySlider.value;
    }

    void Update()
    {
        //On each update the public static sensitivityValue is updated based on the slider's position
        sensitivityValue = sensitivitySlider.value;
        Debug.Log("Current Sensitivity: " + sensitivityValue);
    }
}
