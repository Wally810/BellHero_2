using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteSpawner : MonoBehaviour {

    public float tempo;

    //GameObject Variables for the note prefab objects
    public GameObject notePrefab;
    public GameObject notePrefab1;
    public GameObject notePrefab2;

    // Start is called before the first frame update
    void Start() {
        tempo = tempo / 60f;
    }

    // Update is called once per frame
    void Update() {
        
    }

    //Method to spawn cloneded objects of the note prefabs and change their ID string above them
    public void spawnClone(int noteNum, int variable, string visIDValue){
        //Debug.Log("Clone Spawned");

        //Variable = 1, spawns a "notePrefab" clone
        if(variable == 1){

            //Clone is created and values are assigned to it
            GameObject clonedNote = Instantiate(notePrefab, transform.position, Quaternion.identity, transform);
            clonedNote.GetComponent<NoteObject>().noteFreqNum=noteNum;
            clonedNote.GetComponent<ClonedNoteScroller>().tempo=120;
            clonedNote.GetComponent<ClonedNoteScroller>().isFalling=true;
            clonedNote.GetComponent<NoteObject>().frequencyArray = new int[]{
                890, 930, 990, 1047, 1147, 1178, 1260,
                1317, 1408, 1455, 1570, 1700, 1780, 1870,
                1980, 2080, 2210, 2320, 2470, 2610, 2800};

            //Note ID string text above it is assigned
            TextMeshProUGUI visNoteText = clonedNote.transform.Find("VisNoteID").GetComponent<TextMeshProUGUI>();
            visNoteText.text = visIDValue;

        }
        //Variable = 0, spawns a "notePrefab1" clone
        else if (variable == 0){

            //Clone is created and values are assigned to it
            GameObject clonedNote = Instantiate(notePrefab1, transform.position, Quaternion.identity, transform);
            clonedNote.GetComponent<NoteObject>().noteFreqNum=noteNum;
            clonedNote.GetComponent<ClonedNoteScroller>().tempo=120;
            clonedNote.GetComponent<ClonedNoteScroller>().isFalling=true;
            clonedNote.GetComponent<NoteObject>().frequencyArray = new int[]{
                890, 930, 990, 1047, 1147, 1178, 1260,
                1317, 1408, 1455, 1570, 1700, 1780, 1870,
                1980, 2080, 2210, 2320, 2470, 2610, 2800};

            //Note ID string text above it is assigned
            TextMeshProUGUI visNoteText = clonedNote.transform.Find("VisNoteID").GetComponent<TextMeshProUGUI>();
            visNoteText.text = visIDValue;
        }
        //Variable = 3, spawns a "notePrefab2" clone. This clone is a "Ghost Note", it's invisible and its deletion marks the end of the song
        else if (variable == 3){

            //Clone is created and values are assigned to it
            GameObject clonedNote = Instantiate(notePrefab2, transform.position, Quaternion.identity, transform);
            clonedNote.GetComponent<NoteObject>().noteFreqNum=noteNum;
            clonedNote.GetComponent<ClonedNoteScroller>().tempo=120;
            clonedNote.GetComponent<ClonedNoteScroller>().isFalling=true;
            clonedNote.GetComponent<NoteObject>().ghostNoteObject=true;
            clonedNote.GetComponent<NoteObject>().frequencyArray = new int[]{
                890, 930, 990, 1047, 1147, 1178, 1260,
                1317, 1408, 1455, 1570, 1700, 1780, 1870,
                1980, 2080, 2210, 2320, 2470, 2610, 2800};

            //Note ID string text above it is assigned
            TextMeshProUGUI visNoteText = clonedNote.transform.Find("VisNoteID").GetComponent<TextMeshProUGUI>();
            visNoteText.text = visIDValue;
        }

    }
}
