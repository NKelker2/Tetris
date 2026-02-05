using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIManager : MonoBehaviour {
    public VisualElement ui;

    public Button NextRoundButton;

    private void Awake() {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable() {
        NextRoundButton = ui.Q<Button>("NextRoundButton");
        NextRoundButton.clicked += OnNextRoundButtonClicked;
    }

    private void OnNextRoundButtonClicked() {
        SceneSwap.MoveScenes(0);
    }
}

