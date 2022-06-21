using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    public Text text;

    // public koeficientas, kuris nustato laiko greiti
    // sudauginu su Time.deltaTime
    // is powerup'o koeficientas keiciamas, nes public variable

    public float lapOne = 0f;
    public float lapTwo = 0f;
    public float lapThree = 0f;
    public float lapFour = 0f;

    public float lapTime = 0f;

    public float timeCoef = 1f;

    public float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * timeCoef;
        lapTime += Time.deltaTime * timeCoef;
    }

    public void lap(){
        if(lapOne == 0f){
            lapOne = lapTime;
            text.text += "Lap One: " + lapOne + "\n";
        }
        else if(lapTwo == 0f){
            lapTwo = lapTime;
            text.text += "Lap Two: " + lapTwo + "\n";
        }
        else if(lapThree == 0f){
            lapThree = lapTime;
            text.text += "Lap Three: " + lapThree + "\n";
        }
        else if(lapFour == 0f){
            lapFour = lapTime;
            text.text += "Lap Four: " + lapFour + "\n";
        }
        lapTime = 0f;
    }

    public void reset(){
        lapOne = 0f;
        lapTwo = 0f;
        lapThree = 0f;
        lapFour = 0f;
        lapTime = 0f;
        time = 0f;
        text.text = "";
    }
}
