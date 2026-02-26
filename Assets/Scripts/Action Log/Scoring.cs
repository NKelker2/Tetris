using System;

using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class Scoring : MonoBehaviour
{
    public TextMeshProUGUI scoring;

    //Ordered: blue, cyan, green, orange, purple, red, yellow
    // used for comparison in LineScore method
    public Tile[] colors;

    private double score;

    public int blue, cyan, green, orange, purple, red, yellow;

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
                if (PlayerData.onClearEffects.ContainsKey("red"))
                {
                    for (int j = 0; j < PlayerData.onClearEffects["red"].Count; j++)
                    {
                        lineScore += PlayerData.onClearEffects["red"][j].ApplyEffect(red);

                        Log.PrintToGame("Applied effect");
                    }
                }
            }
            else if (line[i] == colors[6])
                lineScore += yellow;
        }

        foreach (String key in PlayerData.onClearEffects.Keys)
        {
            for (int i = 0; i < PlayerData.onClearEffects[key].Count; i++)
            {
                PlayerData.onClearEffects[key][i].Reset();
            }
        }

        score += lineScore * combo;
        //update score
        scoring.text = "Score: " + score;
        ToShop();

    }

    public void ToShop()
    {
        if (score >= PlayerData.reqScore)
        {
            SceneSwap.MoveScenes(1);

            PlayerData.reqScore *= 2;

            if (PlayerData.currRound % 3 != 0)
                PlayerData.money += 5;
            else
                PlayerData.money += 10;

            PlayerData.currRound++;

        }
    }

    public void ResetScore()
    {
        score = 0;
        scoring.text = "Score: " + score;
    }
}