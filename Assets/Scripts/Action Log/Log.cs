using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using System;
using Unity.VisualScripting;

public class Log : MonoBehaviour {

    public TextMeshProUGUI actionLog;
    public TextMeshProUGUI scoring;

    //lil k additions for scoring
    public double score;
    public int red;
    public int blue;
    public int cyan;
    public int purple;
    public int yellow;
    int lineCount = 0;

    public Log() {
        score = 0;
        red = 5;
        blue = 5;
        cyan = 5;
        purple = 5;
        yellow = 5;
    }

    public void LineScore(Color[] line) {
        double lineScore = 0;

        for (int i = 0; i < line.Length; i++) {
            if (line[i] == Color.red) {
                lineScore += red;
            }
            else if (line[i] == Color.blue) {
                lineScore += blue;
            }
            else if (line[i] == Color.cyan) {
                lineScore += cyan;
            }
            else if (line[i] == Color.purple) {
                lineScore += purple;
            }
            else if (line[i] == Color.yellow) {
                lineScore += yellow;
            }
        }

        score += lineScore;
        //update score
        scoring.text = "Score" + score.ToString();
    }

    // accept a string
    // update collective string for actionlog with new inputs being a new line
    // remove old lines after 10 added lines
    public void printToGame(String input) {
        lineCount++;
    }
}