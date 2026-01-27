using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using System;
using Unity.VisualScripting;

public class Log : MonoBehaviour {

    public TextMeshProUGUI actionLog;
    public TextMeshProUGUI scoring;

    //Ordered: blue, cyan, green, orange, purple, red, yellow
    public Tile[] colors;

    //lil k additions for scoring
    private double score;
    public int blue;
    public int cyan;
    public int green;
    public int orange;
    public int purple;
    public int red;
    public int yellow;
    int lineCount = 0;

    public void LineScore(TileBase[] line, int combo) {
        double lineScore = 0;


        for (int i = 0; i < line.Length; i++) {
            if (line[i] == colors[0])
                lineScore += blue;
            else if (line[i] == colors[1])
                lineScore += cyan;
            else if (line[i] == colors[2])
                lineScore += green;
            else if (line[i] == colors[3])
                lineScore += orange;
            else if (line[i] == colors[4])
                lineScore += purple;
            else if (line[i] == colors[5])
                lineScore += red;
            else if (line[i] == colors[6])
                lineScore += yellow;

        }


        score += lineScore * combo;
        //update score
        scoring.text = "Score: " + score;
    }

    // accept a string
    // update collective string for actionlog with new inputs being a new line
    // remove old lines after 10 added lines
    public void printToGame(String input) {
        lineCount++;
    }
}