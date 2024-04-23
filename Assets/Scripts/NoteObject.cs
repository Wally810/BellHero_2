using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteObject : MonoBehaviour {

    //Some Variables need to be cleaned up as they used to be connected to the
    //soundTransform script and now aren't,

    public bool canBeDeleted = false;
    public float confidenceNumber;
    
    public int startRecording;
    public int recordingLength;

    public int noteFreqNum;

    public float sec;

    public int lengthCounter;

    public bool ghostNoteObject = false;

    public int scoreToSend;
    public GameObject SongReaderObject;
    private SongReader songReader;

    public GameObject freqProcessor;
    public int targetFrequency;

    public SoundTransform soundy;
    public int [] frequencyArray;

    // Start is called before the first frame update
    void Start() {
        startRecording = -1;
        recordingLength = 3148;

        lengthCounter = 0;

        //Debug.Log("Note number: " + noteFreqNum);

        targetFrequency = frequencyArray[noteFreqNum - 1];

        //Debug.Log("Note frequency: " + noteFreq);

        sec = 0.5f;

        SongReaderObject = GameObject.Find("Notes");
        songReader = SongReaderObject.GetComponent<SongReader>();

        freqProcessor = GameObject.Find("Notes");
        soundy = freqProcessor.GetComponent<SoundTransform>();

       frequencyArray = new int[]{
        890, 930, 990, 1047, 1147, 1178, 1260,
        1317, 1408, 1455, 1570, 1700, 1780, 1870,
        1980, 2080, 2210, 2320, 2470, 2610, 2800};
        
    }

    // Update is called once per frame
    void Update() {

        //If the note is the ghost note and reaches the end, the results screen is loaded
        if(ghostNoteObject == true){
            if (transform.position.y <= -Screen.height * .1){

                //Send to results screen with score
                Debug.Log("Song finished, send to results.");

                SceneManager.LoadScene("ResultsScreen");
        
                gameObject.SetActive(false);

            }
        } else {

            //Once the note reaches a designated distance, it can be deleted via the right note being played
            //Or by going out of bounds
            if (canBeDeleted){

                //If the confidenceValue is above 100 
                if(soundy.getFreqLevels(targetFrequency,10) > 100){

                    //If the confidenceValue is above 150
                    if(soundy.getFreqLevels(targetFrequency,10) > 150){

                        //If the confidenceValue is above 150, then 30pts are added to the score
                        //and the relevant Key will display a "Perfect" image above it

                        scoreToSend = 30;
                        SongReader.scoreInt += scoreToSend;

                        string referenceKeyName0 = "Key" + noteFreqNum;
                        GameObject referenceKey0 = GameObject.Find(referenceKeyName0);

                        Debug.Log("Note reaches " + referenceKey0);

                        KeyController keyScript = referenceKey0.GetComponent<KeyController>();
                        keyScript.displayNoteFeedback(2);

                    }else{

                        //If the confidenceValue is above 100, then 15pts are added to the score
                        //and the relevant Key will display a "Good" image above it
                        
                        scoreToSend = 15;
                        SongReader.scoreInt += scoreToSend;

                        string referenceKeyName0 = "Key" + noteFreqNum;
                        GameObject referenceKey0 = GameObject.Find(referenceKeyName0);

                        Debug.Log("Note reaches " + referenceKey0);

                        KeyController keyScript = referenceKey0.GetComponent<KeyController>();
                        keyScript.displayNoteFeedback(1);

                    }

                    //Then the note will delete itself
                    gameObject.SetActive(false);
                }
            }

            //If the note is below a designated percentage of the screen height, then the note can be deleted
            if (transform.position.y <= Screen.height * 0.08f){
                canBeDeleted = true;
            }

            //If the note travels shortly beyond the screen border, then the note is deleted
            if (transform.position.y <= -Screen.height * .1){

                //If the note travels off screen, then no points are added to the score
                //and the relevant Key will display a "Miss" image above it

                string referenceKeyName0 = "Key" + noteFreqNum;
                GameObject referenceKey0 = GameObject.Find(referenceKeyName0);

                Debug.Log("Note reaches " + referenceKey0);

                KeyController keyScript = referenceKey0.GetComponent<KeyController>();
                keyScript.displayNoteFeedback(0);
        
                gameObject.SetActive(false);
            }
        }
        
    }
}
