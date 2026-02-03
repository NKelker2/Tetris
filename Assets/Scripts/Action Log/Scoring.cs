using System;
using System.Collections.Generic;

using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class Scoring : MonoBehaviour
{
    public SceneSwap sceneSwap;

    public Dictionary<String, List<Effect>> onClearEffects;
    public TextMeshProUGUI scoring;

    //Ordered: blue, cyan, green, orange, purple, red, yellow
    // used for comparison in LineScore method
    public Tile[] colors;

    private double score;
    public double reqScore;

    public int blue, cyan, green, orange, purple, red, yellow;

    public Scoring()
    {
        onClearEffects = new Dictionary<string, List<Effect>>();
        sceneSwap = new SceneSwap();
    }

    public void LineScore(TileBase[] line, int combo, int bonusPoints)
    {
        double lineScore = bonusPoints;


        for (int i = 0; i < line.Length; i++)
        {
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
            {
                lineScore += red;
                for (int j = 0; j < onClearEffects["red"].Count; j++)
                {
                    lineScore += onClearEffects["red"][j].ApplyEffect(red);
                }
            }
            else if (line[i] == colors[6])
                lineScore += yellow;
        }

        foreach (String key in onClearEffects.Keys)
        {
            for (int i = 0; i < onClearEffects[key].Count; i++)
            {
                onClearEffects[key][i].Reset();
            }
        }

        score += lineScore * combo;
        //update score
        scoring.text = "Score: " + score;
        ToShop();

    }

    /*
    public void scoreneeded() {
        if (bossfight)
        {
            reqscore = reqScore * 3;
        }
        else
        {
            reqscore = reqScore * 2;
        }
    }
    */

    public void ToShop()
    {
        if (score >= reqScore)
        {
            sceneSwap.MoveScenes(0);
        }
    }

}