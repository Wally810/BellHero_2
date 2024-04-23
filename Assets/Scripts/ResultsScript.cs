using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsScript : MonoBehaviour
{
    //Method to load the SongSelectionScreen when the attached button is clicked
    public void loadSongSelectionScreen(){
        SceneManager.LoadScene("SongSelectionScreen");
    }

    //Method to load the GameScreen when the attached button is clicked
    public void loadGameScreen(){
        SceneManager.LoadScene("GameScreen");
    }

    void Start() {

        //On start, the text object to display the score is assigned
        GameObject displayScore = GameObject.Find("TotalScoreText");;
        TextMeshProUGUI displayScoreText = displayScore.GetComponent<TextMeshProUGUI>();

        //The displayed value of the score is assigned as the static value stored in the SongReader script
        displayScoreText.text = "Total Score... " + SongReader.scoreInt + "pts";


        //On start, the text object to display the accuracy is assigned
        GameObject accuracyScore = GameObject.Find("AccuracyText");;
        TextMeshProUGUI accuracyScoreText = accuracyScore.GetComponent<TextMeshProUGUI>();

        //Accuracy float variable created
        float accuracyScoreTotal;

        //Debug.Log("SongReader.totalNotes: " + SongReader.totalNotes);
        //Debug.Log("SongReader.totalNotes * 30: " + SongReader.totalNotes * 30);
        //Debug.Log("SongReader.scoreInt: " + SongReader.scoreInt);
        //Debug.Log(SongReader.scoreInt + " / " + SongReader.totalNotes);

        //The score is divided by the highest possible score to get an accuracy percentage
        accuracyScoreTotal = (float)SongReader.scoreInt / (float)((SongReader.totalNotes) * 30);
    
        //Debug.Log("Accuracy Total: " + accuracyScoreTotal);

        //If the accuracy is above 100% it is set to 100%
        if(accuracyScoreTotal > 100){
            accuracyScoreTotal = 100;
        }

        //The displayed value of the accuracy is assigned from the accuracy value calculated
        accuracyScoreText.text = "Accuracy...  " + (accuracyScoreTotal * 100f).ToString("0.00") + "%" ;

    }
}
