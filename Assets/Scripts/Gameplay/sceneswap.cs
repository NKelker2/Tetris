using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwap {

    public static void MoveScenes(int x) {
        Log.PrintToGame("Switched scene");
        SceneManager.LoadScene(x);
    }

    //need to put in some sort of gameloop

}