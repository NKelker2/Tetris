using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIManager : MonoBehaviour {
    public ShopData shopData;

    public VisualElement shopUI;
    public ColorSelectManager ColorSelect;

    public Button nextRoundButton;

    public Button rerollButton;

    public Button shopSlot1;
    public String shopSlot1Name;
    public Label shopSlot1Price;

    public Button tokenSlot1;
    public String tokenSlot1Name;
    public Label tokenSlot1Price;


    private void Awake() {
        shopUI = GetComponent<UIDocument>().rootVisualElement;

        shopData = GetComponent<UIDocument>().GetComponentInChildren<ShopData>();

        ColorSelect.colorSelectUI.SetEnabled(false);
        ColorSelect.colorSelectUI.AddToClassList("hidden");
    }

    private void OnEnable() {
        nextRoundButton = shopUI.Q<Button>("NextRoundButton");
        nextRoundButton.clicked += OnNextRoundButtonClicked;

        shopSlot1 = shopUI.Q<Button>("ShopSlot1");
        shopSlot1.clicked += ShopSlot1Bought;

        tokenSlot1 = shopUI.Q<Button>("TokenSlot1");
        tokenSlot1.clicked += TokenSlot1Bought;

        shopSlot1Price = shopUI.Q<Label>("ShopSlot1Price");

        tokenSlot1Price = shopUI.Q<Label>("TokenSlot1Price");

        rerollButton = shopUI.Q<Button>("RerollButton");
        rerollButton.clicked += RerollButtonClicked; ;

        // initializing shop items
        RerollButtonClicked();
    }

    private void OnNextRoundButtonClicked() {
        SceneSwap.MoveScenes(0);
    }

    private void ShopSlot1Bought() {
        if (PlayerData.money >= int.Parse(shopSlot1Price.text.Substring(1))) {
            shopSlot1.SetEnabled(false);
            BuyItem(shopSlot1Name);
            PlayerData.money -= int.Parse(shopSlot1Price.text.Substring(1));
        }
    }

    private void TokenSlot1Bought() {
        if (PlayerData.money >= int.Parse(tokenSlot1Price.text.Substring(1))) {
            ColorSelect.tokenName = tokenSlot1Name;

            ColorSelect.colorSelectUI.SetEnabled(true);
            ColorSelect.colorSelectUI.RemoveFromClassList("hidden");

            tokenSlot1.SetEnabled(false);
            PlayerData.money -= int.Parse(tokenSlot1Price.text.Substring(1));
        }
    }

    private void RerollButtonClicked() {
        EffectSlotsRerolled(shopSlot1);
        TokenSlotsRerolled(tokenSlot1);
    }

    private void EffectSlotsRerolled(Button ShopSlot) {
        // rarity 1-5 = common, 6-9 = uncommon, 10 = rare for later implementation
        int rarity = UnityEngine.Random.Range(1, 5);
        int price;
        Sprite shopItem;

        /*
        if (rarity == 10) {
            shopItem = shopData.rareEffects[UnityEngine.Random.Range(0, shopData.commonEffects.Count())];
            price = 5
        }
        else if (rarity >= 6) {
            shopItem = shopData.uncommonEffects[UnityEngine.Random.Range(0, shopData.commonEffects.Count())];
            price = 3
        }
        else { */
        shopItem = shopData.commonEffects[UnityEngine.Random.Range(0, shopData.commonEffects.Length)];
        price = 2;
        //}

        ShopSlot.SetEnabled(true);

        ShopSlot.style.backgroundImage = new StyleBackground(shopItem.texture);

        if (ShopSlot == shopSlot1) {
            shopSlot1Name = shopItem.name;
            shopSlot1Price.text = "$" + price.ToString();
        }
    }

    private void TokenSlotsRerolled(Button TokenSlot) {
        // rarity 1-5 = common, 6-9 = uncommon, 10 = rare for later implementation
        Sprite shopItem;

        shopItem = shopData.tokens[UnityEngine.Random.Range(0, shopData.commonEffects.Length)];

        TokenSlot.SetEnabled(true);

        TokenSlot.style.backgroundImage = new StyleBackground(shopItem.texture);

        if (TokenSlot == tokenSlot1) {
            tokenSlot1Name = shopItem.name;
            tokenSlot1Price.text = "$" + 3.ToString();
        }
    }

    private void BuyItem(String itemName) {
        switch (itemName) {
            case "RedCombo_0": // happens when item bought is red combo, would add to playerData effects list
                EffectsManager.AddOnClearEffect(new RedCombo());
                break;
        }
    }
}

