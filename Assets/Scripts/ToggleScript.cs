using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public GameObject NoteNamesCheck;
    public GameObject FlatsCheck;
    public static bool isFlat;
    public static bool isNumber;

    public void Start()
    {
        NoteNamesCheck.gameObject.SetActive(false);
        FlatsCheck.gameObject.SetActive(false);
        isNumber = true;
        isFlat = false;
    }

    public void ToggleNoteNames()
    {
       if(isNumber){ 
        // Number -> Names
        NoteNamesCheck.SetActive(true);
        isNumber = false;
       }else{
        // Names -> Number
        NoteNamesCheck.SetActive(false);
        isNumber = true;
       }
    }

    public void ToggleFlats()
    { 
        if(!isFlat){
            // Sharp -> Flat
            FlatsCheck.SetActive(true);
            isFlat = true;
        }else { 
            // Flat -> Sharp
            FlatsCheck.SetActive(false);
            isFlat = false;
        }
    }

    
}
