using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIManager : MonoBehaviour {
    public VisualElement ShopUI;

    public Button NextRoundButton;
    public Button ShopSlot1;
    public Button RerollButton;

    private void Awake() {
        ShopUI = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable() {
        NextRoundButton = ShopUI.Q<Button>("NextRoundButton");
        NextRoundButton.clicked += OnNextRoundButtonClicked;

        ShopSlot1 = ShopUI.Q<Button>("ShopSlot1");
        ShopSlot1.clicked += ShopSlot1Bought;

        RerollButton = ShopUI.Q<Button>("RerollButton");
        RerollButton.clicked += RerollButtonClicked;

        //initializing shop items
        ShopSlot1.text = UnityEngine.Random.Range(0, 10).ToString();
    }

    private void OnNextRoundButtonClicked() {
        SceneSwap.MoveScenes(0);
    }

    private void ShopSlot1Bought() {
        ShopSlot1.SetEnabled(false);
    }

    private void RerollButtonClicked() {
        ShopRerolled();
    }

    private void ShopRerolled() {
        ShopSlot1.SetEnabled(true);

        ShopSlot1.text = UnityEngine.Random.Range(0, 10).ToString();
    }
}

