using UnityEngine;
using UnityEngine.UIElements;

using System;
using System.Xml.Serialization;

public class ColorSelectManager : MonoBehaviour {
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
        BuyToken("red");
    }

    private void BuyToken(String location) {
        switch (tokenName) {
        }

        colorSelectUI.SetEnabled(false);
        colorSelectUI.AddToClassList("hidden");
    }
}
