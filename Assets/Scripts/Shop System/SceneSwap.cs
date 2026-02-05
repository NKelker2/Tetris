using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwap {
    public static void MoveScenes(int x) {
        SceneManager.LoadScene(x);
    }
}