using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    Log log;

    public SceneSwap(Log log)
    {
        this.log = log;

    }

    public void MoveScenes(int x)
    {
        log.PrintToGame("Switched scene");
        SceneManager.LoadScene(x);
    }

    //need to put in some sort of gameloop

}