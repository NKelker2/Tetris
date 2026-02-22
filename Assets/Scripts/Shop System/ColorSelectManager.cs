using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;

using System;
using System.Collections.Generic;


public class ColorSelectManager : MonoBehaviour {

    /*
    index 0: RedToken
    */
    public Tile[] tokenIcons;
    //Ordered: blue, cyan, green, orange, purple, red, yellow
    public Tile[] tetrominoes;

    public VisualElement colorSelectUI;
    public ShopUIManager shopUIManager;

    public Button redButton;

    public String tokenName;

    void Awake() {
        colorSelectUI = GetComponent<UIDocument>().rootVisualElement;
    }

    void OnEnable() {
        redButton = colorSelectUI.Q<Button>("RedButton");
        redButton.clicked += RedChosen;
    }

    private void RedChosen() {
        BuyToken(tetrominoes[5]);
    }

    private void BuyToken(Tile color) {
        switch (tokenName) {
            case "Red_Token_0":
                AddToken(new RedToken(tokenIcons[0]), color);
                break;
        }

        colorSelectUI.SetEnabled(false);
        colorSelectUI.AddToClassList("hidden");
    }

    private void AddToken(Token token, Tile color) {
        if (!PlayerData.onClearTokens.ContainsKey(color))
            PlayerData.onClearTokens.Add(color, new List<Token>());

        token.SetCellPosition(PlayerData.onClearTokens[color].Count);

        PlayerData.onClearTokens[color].Add(token);
        PlayerData.allTokens.Add(token.icon, token);
    }
}
