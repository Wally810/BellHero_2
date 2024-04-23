using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyController : MonoBehaviour{

    //Object and object image variables
    private Image theImage;
    public Sprite defaultImage;
    public Sprite pressedImage;
    
    //Boolean for if a note is used in the song or not
    public bool noteIsUsed;
    public bool scoreChange;

    //Image variables for a note "miss", "good" score, and "perfect" score
    public Sprite miss;
    public Sprite good;
    public Sprite perfect;

    // Start is called before the first frame update
    void Start(){

        //The key's image is assigned to a variable
        theImage = GetComponent<Image>();

        //The key's child object image is assigned to a variable. This child is responsible for the
        //score text such as "miss" or "perfect"
        Image childImage = transform.Find("Image").GetComponent<Image>();
        //The child is set to inactive, making it invisible
        childImage.enabled = false;
    }

    void Update(){
        //This if/else statement replaces the key image with a darkend one if the note isn't used in
        //the song at all
        if (noteIsUsed == true)
        {
            theImage.sprite = defaultImage;
        }
        else
        {
            theImage.sprite = pressedImage;
        }
    }

    //This method handles displaying the "miss", "good", and "perfect" child images
    public void displayNoteFeedback(int feedback){

        //Child object assigned
        Image childImage = transform.Find("Image").GetComponent<Image>();


        //Debug.Log("Note reaches with " + feedback);

        //If the value 0 is given from a note before it's deleted, "miss" displays
        if(feedback == 0){
            childImage.sprite = miss;
            childImage.enabled = true;
            //Coroutine to hide the foodback starts
            StartCoroutine (HideFeedback());
        }
        //If the value 1 is given from a note before it's deleted, "good" displays
        if(feedback == 1){
            childImage.sprite = good;
            childImage.enabled = true;
            //Coroutine to hide the foodback starts
            StartCoroutine (HideFeedback());
        }
        //If the value 2 is given from a note before it's deleted, "perfect" displays
        if(feedback == 2){
            childImage.sprite = perfect;
            childImage.enabled = true;
            //Coroutine to hide the foodback starts
            StartCoroutine (HideFeedback());
        }
    }

    //This coroutine handles the hiding of the feedback image after half a second of display
    IEnumerator HideFeedback(){

        //Child object is assigned
        Image childImage = transform.Find("Image").GetComponent<Image>();

        //Coroutine waits for half a second
        yield return new WaitForSeconds(0.5f);

        //The childImage is deactivated, hiding the feedback
        childImage.enabled = false;
    }
}
