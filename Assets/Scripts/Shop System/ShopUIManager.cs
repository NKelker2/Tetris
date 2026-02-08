using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIManager : MonoBehaviour {
    private ShopData shopData;

    public VisualElement ShopUI;

    public Button NextRoundButton;

    public Button RerollButton;

    public Button ShopSlot1;
    public String ShopSlot1Name;
    public Label ShopSlot1Price;


    private void Awake() {
        ShopUI = GetComponent<UIDocument>().rootVisualElement;

        shopData = GetComponent<UIDocument>().GetComponentInChildren<ShopData>();
    }

    private void OnEnable() {
        NextRoundButton = ShopUI.Q<Button>("NextRoundButton");
        NextRoundButton.clicked += OnNextRoundButtonClicked;

        ShopSlot1 = ShopUI.Q<Button>("ShopSlot1");
        ShopSlot1.clicked += ShopSlot1Bought;

        ShopSlot1Price = ShopUI.Q<Label>("ShopSlot1Price");

        RerollButton = ShopUI.Q<Button>("RerollButton");
        RerollButton.clicked += RerollButtonClicked; ;

        // initializing shop items
        RerollButtonClicked();
    }

    private void OnNextRoundButtonClicked() {
        SceneSwap.MoveScenes(0);
    }

    private void ShopSlot1Bought() {
        if (PlayerData.money >= int.Parse(ShopSlot1Price.text.Substring(1))) {
            ShopSlot1.SetEnabled(false);
            BuyItem(ShopSlot1Name);
            PlayerData.money -= int.Parse(ShopSlot1Price.text.Substring(1));
        }
    }

    private void RerollButtonClicked() {
        EffectSlotsRerolled(ShopSlot1);
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
        shopItem = shopData.commonEffects[UnityEngine.Random.Range(0, shopData.commonEffects.Count())];
        price = 2;
        //}

        ShopSlot.SetEnabled(true);

        ShopSlot.style.backgroundImage = new StyleBackground(shopItem.texture);

        if (ShopSlot == ShopSlot1) {
            ShopSlot1Name = shopItem.name;
            ShopSlot1Price.text = "$" + price.ToString();
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

