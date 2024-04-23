using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System;

public class SongReader : MonoBehaviour
{

    public string songLocation = "Assets/Songs/" + SongSelectionScript.ClickedButtonName + ".txt";
    //public string songLocation = "Assets/Songs/testSong.txt";

    //tempo values
    public float tempo = 120;
    public float tempoDelay = 0;

    //Variables related to the score are created
    public GameObject scoreTextGameObject;
    public TextMeshProUGUI scoreTextObject;
    public string songScore = "SCORE: ";
    public string songScoreText;
    public static int scoreInt = 0;
    public static int totalNotes = 0;

    //Array for notes used in the song
    public  int[] usedNotesArray;

    //String for the song's title to be displayed
    public string songTitle;

    //Objects to reach the song's title text object
    public GameObject titleTextGameObject;
    public TextMeshProUGUI titleTextObject;

    //String for the song's used notes to display below the title
    public string songTitleNotes;

    //Objects to reach the song's title's used notes text object
    public GameObject songTitleNotesTextGameObject;
    public TextMeshProUGUI songTitleNotesTextObject;

    //Booleans for how the song notes will be displayed
    public bool noteNameToggle = false;
    public bool flatsToggle = false;

    void Start()
    {
        songLocation = "Assets/Songs/" + SongSelectionScript.ClickedButtonName + ".txt";
        //songLocation = "Assets/Songs/testSong.txt";

        //Debug.Log(songLocation + " has been recieved");

        //Gets the song's title to be displayed from the file name supplied from SongSelectionScript
        songTitle = SongSelectionScript.ClickedButtonName;
        songTitle = songTitle.Replace("_", " ");
        
        //Has the Song Title be converted to Title Case
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        songTitle = textInfo.ToTitleCase(songTitle.ToLower());

        //Debug.Log(songTitle);

        //The three below lines replace the song title in the gamescreen with the song's file
        titleTextGameObject = GameObject.Find("SongTitleText");
        titleTextObject = titleTextGameObject.GetComponent<TextMeshProUGUI>();

        titleTextObject.text = songTitle;

        //Two lines to reach the used notes text object

        songTitleNotesTextGameObject = GameObject.Find("SongTitleNotesText");
        songTitleNotesTextObject = songTitleNotesTextGameObject.GetComponent<TextMeshProUGUI>();
        
        //Create Function To Scan Song For Used Notes
        usedNotes(usedNotesArray, ref songTitleNotes);

        //The text describing the notes used is assigned to a variable
        songTitleNotesTextObject.text = "Notes: "+ songTitleNotes;

        //Booleans for note values are pulled from the options screen
        noteNameToggle = !ToggleScript.isNumber;
        flatsToggle = ToggleScript.isFlat;

        //Key text values are updated based on above booleans as well
        updateKeyNames(noteNameToggle, flatsToggle);

        //Score values are initialized
        songScore = "SCORE: ";
        scoreInt = 0;
        totalNotes = 0;

        //Score text object for ingame display is reached
        scoreTextGameObject = GameObject.Find("ScoreText");
        scoreTextObject = scoreTextGameObject.GetComponent<TextMeshProUGUI>();

        //Coroutine to deleted song title and notes used is started
        StartCoroutine (DeleteTitleCoroutine());

        //Coroutine to output song is started
        StartCoroutine (OutputSong());
    }

    
    //Method to find the notes used in a song.txt file 
    void usedNotes( int[] usedNotesArray, ref string songTitleNotes){
        if (File.Exists(songLocation))
        {
            //A list is created from the empty array
            List<int> usedNotesList = new List<int>(usedNotesArray ?? new int[0]);
            
            foreach (string songLine in File.ReadLines(songLocation))
            {
                if(songLine != "*"){
                    //Split text into an array using dash to know where to split
                    string[] lineNotes = songLine.Split('-');

                    foreach (string SongNote in lineNotes)
                    {
                        int pulledNote = int.Parse(SongNote);

                        if(!usedNotesList.Contains(pulledNote)){
                            usedNotesList.Add(pulledNote);
                        }
                    }
                }
            }

            //The list is moved back to the array and sorted
            usedNotesArray = usedNotesList.ToArray();
            Array.Sort(usedNotesArray);

            //The string of all used notes is created from the sorted array
            songTitleNotes = string.Join(", ", usedNotesArray);
            Debug.Log(string.Join(", ", usedNotesArray));

            //This for loop goes through each key value and, referencing the values in the
            //usedNotesArray, darkens unused keys while keeping the used keys light
            for(int i = 1; i <= 21; i ++){
                if(Array.Exists(usedNotesArray, note => note == i)){
                    string referenceKeyName0 = "Key" + i;
                    GameObject referenceKey0 = GameObject.Find(referenceKeyName0);

                    //Debug.Log(referenceKeyName0);

                    KeyController keyScript = referenceKey0.GetComponent<KeyController>();
                    keyScript.noteIsUsed = true;
                    //Debug.Log(referenceKey0 + "is set to true");
                }
            }
        }
        else
        {
            Debug.LogError("File not found: " + songLocation);
        }
    }

    //This method updates the text of each key object based on the options booleans
    void updateKeyNames(bool noteNameToggle, bool flatsToggle){

        string referenceKeyName;
        GameObject referenceKey;
        TextMeshProUGUI referenceKeyText;

        //String array for the notes if they were sharps
        string[] notesArraySharps = new string[]{
            "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#",
            "A", "A#", "B", "C", "C#", "D", "D#", "E", "F"
        };

        //String array for the notes if they were flats
        string[] notesArrayFlats = new string[]{
            "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab",
            "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F"
        };

        //This loops through each key's child text object and assigns the appropriate text
        for(int i = 1; i <= 21; i++){
            referenceKeyName = "VisKeyID" + i;
            referenceKey = GameObject.Find(referenceKeyName);

            referenceKeyText = referenceKey.GetComponent<TextMeshProUGUI>();

            //If noteNameToggle is false, the keys are assigned numbers
            if(noteNameToggle == false){
                referenceKeyText.text = i.ToString();
            }
            else{
                //If noteNametoggle is true and flatsToggle is false, the keys are assigned from the sharps array
                if(flatsToggle == false){
                    referenceKeyText.text = notesArraySharps[i-1];
                }
                //If noteNametoggle is true and flatsToggle is true, the keys are assigned from the flats array
                else{
                    referenceKeyText.text = notesArrayFlats[i-1];
                }
            }
        }
    }

    //The coroutine for outputing the song 
    IEnumerator OutputSong(){
        tempo = 120/60f;
        tempoDelay = tempo;

        if (File.Exists(songLocation))
        {
            
            foreach (string songLine in File.ReadLines(songLocation))
            {

                yield return new WaitForSeconds(tempo/4);
                //Debug.Log("Tempo Delay: " + tempoDelay);
                totalNotes += 1;

                if(songLine != "*"){
                    //Split text into an array using dash to know where to split
                    string[] lineNotes = songLine.Split('-');

                    foreach (string SongNote in lineNotes)
                    {
                        //Debug.Log("Song note: " + SongNote+ "With Tempo Delay: " + tempoDelay);
                        DelayCoroutine(tempoDelay, SongNote, noteNameToggle, flatsToggle);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("File not found: " + songLocation);
        }
        //Ghostnote to end
        StartCoroutine (DelayCoroutine(tempoDelay));
    }

    void Update(){

        Debug.Log(SoundTransform.sensitivity);
        
        //Updates game score text
        songScoreText = songScore + scoreInt.ToString("D4");
        scoreTextObject.text = songScoreText;
    }

    //Method was a coroutine but now just a method that spawns a note prefab
    void DelayCoroutine(float tempo, string note, bool noteNameToggle, bool flatsToggle){
        int noteNum = int.Parse(note);

        string referenceNoteName = "Note" + noteNum;
        GameObject referenceNote = GameObject.Find(referenceNoteName);

        NoteSpawner spawnClone = referenceNote.GetComponent<NoteSpawner>();

        string visIDValue;

        string[] notesArraySharps = new string[]{
            "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#",
            "A", "A#", "B", "C", "C#", "D", "D#", "E", "F"
        };

        string[] notesArrayFlats = new string[]{
            "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab",
            "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F"
        };

        //If/else statements that handle text assignement for the falling notes
        //based on the boolean values in the options screen
        if(noteNameToggle == false){
            visIDValue = noteNum.ToString();
        }
        else{
            if(flatsToggle == false){
                visIDValue = notesArraySharps[noteNum - 1];
            }
            else{
                visIDValue = notesArrayFlats[noteNum-1];
            }
        }

        if (noteNum % 2 == 0){
            spawnClone.spawnClone(noteNum, 0, visIDValue);
        }
        else{
            spawnClone.spawnClone(noteNum, 1, visIDValue);
        }
    }

    //Spawns the invisible Ghost Note to end the song
    IEnumerator DelayCoroutine(float tempo){
        yield return new WaitForSeconds(tempo);

        //Debug.Log("Delayed");

        string referenceNoteName = "Note" + 1;
        GameObject referenceNote = GameObject.Find(referenceNoteName);

        NoteSpawner spawnClone = referenceNote.GetComponent<NoteSpawner>();

        string visIDValue = "WompWomp";

        spawnClone.spawnClone(1, 3, visIDValue);
    }

    //Method that deletes the song title after 4 seconds
    IEnumerator DeleteTitleCoroutine(){
        yield return new WaitForSeconds(4);

        GameObject songTitleGameObject = GameObject.Find("SongTitle");

        songTitleGameObject.SetActive(false);
    }
}
