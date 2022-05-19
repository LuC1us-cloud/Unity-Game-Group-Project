using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public void DisplayScores(){
        
        var currentScore = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Score>().score;
        var Highscore = PlayerPrefs.GetInt("highscore", 0);
    
        if (currentScore > Highscore){
            Highscore = currentScore;
        }
        var finalString = $"Current score: {currentScore} \nHighscore: {Highscore}";
        text.text = finalString;

        PlayerPrefs.SetInt("highscore", Highscore);
    }
}
