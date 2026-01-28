using System;
using System.Collections.Generic;

using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class Scoring : MonoBehaviour {

    public Dictionary<String, List<Effect>> onClearEffects;

    public TextMeshProUGUI scoring;

    //Ordered: blue, cyan, green, orange, purple, red, yellow
    // used for comparison in LineScore method
    public Tile[] colors;

    private String log;

    //lil k additions for scoring
    private double score;
    public int blue;
    public int cyan;
    public int green;
    public int orange;
    public int purple;
    public int red;
    public int yellow;

    public Scoring() {
        onClearEffects = new Dictionary<string, List<Effect>>();
    }

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
            else if (line[i] == colors[5]) {
                lineScore += red;
                for (int j = 0; j < onClearEffects["red"].Count; j++) {
                    lineScore += onClearEffects["red"][j].ApplyEffect(red);
                }
            }
            else if (line[i] == colors[6])
                lineScore += yellow;
        }

        foreach (String key in onClearEffects.Keys) {
            for (int i = 0; i < onClearEffects[key].Count; i++) {
                onClearEffects[key][i].Reset();
            }
        }

        score += lineScore * combo;
        //update score
        scoring.text = "Score: " + score;
    }
}