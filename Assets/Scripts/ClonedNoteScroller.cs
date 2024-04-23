using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonedNoteScroller : MonoBehaviour
{
    public float tempo;

    //Variables for the note to fall
    public RectTransform clonedNoteRectTransform;
    public bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //As long as the "isFalling" variable is true, the note knows to keep following at the pace of the tempo value
        if(isFalling == true){
            clonedNoteRectTransform.anchoredPosition -= new Vector2(0f, tempo * Time.deltaTime);
        }
    }
}
