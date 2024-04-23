using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransform : MonoBehaviour
{
    public static int samplingFreq;
    public static float sensitivity = OptionsScreenScript.sensitivityValue;
    private AudioSource audioSource;
    public static float[] samples;
    public static complexNumber[][] DFTmatrix;
    public static complexNumber[][] freqOverTimeMatrix;

    public static int currentFreqIndex;
    public static int DFTmatrixSize = 1000;
    public static int DFTmatrixSkipping = 3;
    public static int DFTtimeSize = 500;

    public static int[] frequencyArray = new int[]{
        890, 930, 990, 1047, 1147, 1178, 1260,
        1317, 1408, 1455, 1570, 1700, 1780, 1870,
        1980, 2080, 2210, 2320, 2470, 2610, 2800
    };

    public static bool firstRunDone = false;
    void Start()
    {
        Debug.Log(Microphone.devices[0]);
        Microphone.GetDeviceCaps("",out int minCap, out int maxCap);
        Debug.Log(minCap + " " + maxCap);
        samplingFreq = maxCap;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, samplingFreq);
        samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.loop = true;
        createTailoredDFTmatrix();
        freqOverTimeMatrix = new complexNumber[samplingFreq/(DFTtimeSize)][];
        for(int index = 0; index<samplingFreq/(DFTtimeSize); index++){
            freqOverTimeMatrix[index] = new complexNumber[DFTmatrixSize];
            for(int indexW = 0; indexW<DFTmatrixSize; indexW++){
                freqOverTimeMatrix[index][indexW] = new complexNumber(0.0,0.0);
            }
        }
    }
    public static void freqFinderComplex(int foundIndex){
        complexNumber[] convertedSamples = new complexNumber[DFTtimeSize];
        complexNumber[] foundFrequencies = new complexNumber[DFTtimeSize];
        //int highestIndex = 0;

        for(int tIndex = 0; tIndex < DFTtimeSize; tIndex++){
            convertedSamples[tIndex] = new complexNumber(0.0f,samples[(tIndex+foundIndex*DFTtimeSize)%samplingFreq]);
            foundFrequencies[tIndex] = new complexNumber(0.0f, 0.0f);
        }
        for(int mIndex = 0; mIndex < DFTmatrix.Length; mIndex++){   
            //Debug.Log(foundFrequencies[mIndex].toString());         
            for (int pos = 0; pos < DFTtimeSize; pos++)
            {
                foundFrequencies[mIndex].addToThis(
                    DFTmatrix[mIndex][pos].multiplyWithThis(
                        convertedSamples[pos]
                    )
                );
            }
        }
        //Debug.Log(foundIndex);
        freqOverTimeMatrix[foundIndex] = foundFrequencies;
        firstRunDone = true;
        //Debug.Log(3*highestIndex);
    }
    public static void createDFTmatrix(int matrixSize,int matrixSkipping){
        DFTmatrix = new complexNumber[matrixSize][];
        for(int mIndex = 0; mIndex < matrixSize; mIndex++){
            DFTmatrix[mIndex] = new complexNumber[DFTtimeSize];
            for(int tIndex = 0; tIndex < DFTtimeSize; tIndex++){
                DFTmatrix[mIndex][tIndex] = new complexNumber(2*Math.PI/samplingFreq*tIndex*mIndex*matrixSkipping);
            }
        }
    }
    public static void createTailoredDFTmatrix(){
        DFTmatrix = new complexNumber[frequencyArray.Length][];
        Debug.Log("DFT Size: "+DFTmatrix.Length);
        for(int mIndex = 0; mIndex < frequencyArray.Length; mIndex++){
            DFTmatrix[mIndex] = new complexNumber[DFTtimeSize];
            for(int tIndex = 0; tIndex < DFTtimeSize; tIndex++){
                DFTmatrix[mIndex][tIndex] = new complexNumber(
                    2*Math.PI/samplingFreq*tIndex*frequencyArray[mIndex]
                );
            }
        }
    }
    public double getFreqLevels(int targetFrequency, int freqRange){
        double runningValue = 0.0;
        //Debug.Log(targetFrequency);
        for(int index = 0; index < frequencyArray.Length; index++){
            if(frequencyArray[index]==targetFrequency){
                //Debug.Log(freqOverTimeMatrix[currentFreqIndex][index].magnitude());
                return sensitivity*freqOverTimeMatrix[currentFreqIndex][index].magnitude();
            }
        }
        /*
        for(int index = -(int)freqRange/(int)DFTmatrixSkipping; index<(int)freqRange/(int)DFTmatrixSkipping; index++){
            runningValue += freqOverTimeMatrix[currentFreqIndex][(int)(targetFrequency/DFTmatrixSkipping)+index].magnitude();
        }
        */
        return runningValue;
    }
    // Update is called once per frame
    
    void Update()
    {
        audioSource.clip.GetData(samples, 0);
        if(Microphone.GetPosition(null)>(DFTtimeSize)*(currentFreqIndex+1)){
            freqFinderComplex(currentFreqIndex);
            currentFreqIndex++;
        }else if(Microphone.GetPosition(null) < (DFTtimeSize)*(currentFreqIndex)){
            if((Microphone.GetPosition(null)+samplingFreq)>(DFTtimeSize)*(currentFreqIndex+1)){
                freqFinderComplex(currentFreqIndex);
                currentFreqIndex = 0;
            }
        }
        //Debug.Log(getFreqLevels(890, 10));
    }
    
}
public class complexNumber{
    double realComp;
    double imagComp;
    public complexNumber(){
        //Empty Case
        realComp = 0.0f;
        imagComp = 0.0f;
    }
    public complexNumber(double realComp, double imagComp){
        //Creating the complex number via manual entry
        this.realComp = realComp;
        this.imagComp = imagComp;
    }
    public complexNumber(double theta){
        //Creating the complex number via Euler's formula
        realComp = Math.Cos(theta);
        imagComp = Math.Sin(theta);
    }
    public complexNumber addWithThis(complexNumber adder){
        return new complexNumber(
            adder.realComp + realComp, 
            adder.imagComp + imagComp
        );
    }
    public complexNumber addWithThis(double realComp, double imagComp){
        return new complexNumber(
            this.realComp + realComp, 
            this.imagComp + imagComp
        );
    }
    public complexNumber subWithThis(complexNumber subtracter){
        return new complexNumber(
            this.realComp + realComp, 
            this.imagComp + imagComp
        );
    }
    public complexNumber subWithThis(double realComp, double imagComp){
        return new complexNumber(
            this.realComp + realComp, 
            this.imagComp + imagComp
        );
    }
    public complexNumber multiplyWithThis(complexNumber multiplier){
        return new complexNumber(
            realComp * multiplier.realComp - imagComp * multiplier.imagComp, 
            imagComp * multiplier.realComp + realComp * multiplier.imagComp
        );
    }
    public complexNumber multiplyWithThis(double multiplier){
        return new complexNumber(realComp * multiplier, imagComp * multiplier);
    }

    public void addToThis(complexNumber adder){
        realComp = adder.realComp + realComp;
        imagComp = adder.imagComp + imagComp;
    }
    public void addToThis(double realComp, double imagComp){
        this.realComp = realComp + realComp;
        this.imagComp = imagComp + imagComp;
    }
    public void subFromThis(complexNumber subtracter){
        realComp = realComp - subtracter.realComp;
        imagComp = imagComp - subtracter.imagComp;
    }
    public void subFromThis(double realComp, double imagComp){
        this.realComp = realComp - realComp;
        this.imagComp = imagComp - imagComp;
    }
    public void multiplyThisBy(complexNumber multiplier){
        realComp = realComp * multiplier.realComp - imagComp * multiplier.imagComp;
        imagComp = imagComp * multiplier.realComp + realComp * multiplier.imagComp;
    }
    public void multiplyThisBy(double multiplier){
        realComp = realComp * multiplier;
        imagComp = imagComp * multiplier;
    }
    public String toString(){
        return (realComp.ToString())+" + "+(imagComp.ToString())+"i";
    }
    public double magnitude(){
        return Math.Sqrt(Math.Abs(realComp*realComp - imagComp*imagComp));
    }
}