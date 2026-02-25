using System;
using System.Collections.Generic;

using UnityEngine.Tilemaps;

public static class PlayerData {
    public static Dictionary<String, List<Effect>> onClearEffects = new Dictionary<string, List<Effect>>();

    // Tile key will be the color they will spawn on
    // Might change name to just tokensByColor since all tokens will have effects that activate on clear
    public static Dictionary<Tile, List<Token>> onClearTokens = new Dictionary<Tile, List<Token>>();
    public static Dictionary<TileBase, Token> allTokens = new Dictionary<TileBase, Token>();

    public static int currRound = 2;

    public static double reqScore = 500;
    public static double money = 100;
}
