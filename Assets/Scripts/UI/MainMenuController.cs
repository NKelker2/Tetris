using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour {
    public VisualElement ui;

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    private void Awake() {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable() {
        playButton = ui.Q<Button>("StartButton");
        playButton.clicked += OnPlayButtonClicked;

        optionsButton = ui.Q<Button>("OptionsButton");
        playButton.clicked += OnOptionsButtonClicked;

        quitButton = ui.Q<Button>("QuitButton");
        playButton.clicked += OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked() {
        SceneSwap.MoveScenes(0);
    }

    private void OnOptionsButtonClicked() {

    }

    private void OnQuitButtonClicked() {
        Application.Quit();
    }
}
