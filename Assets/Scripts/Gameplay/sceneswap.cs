using UnityEngine.SceneManagement;

public class sceneswap : MonoBehavior
{
    public void movescenes(int x)
    {
        SceneManager.LoadScene(x);
    }

    //need to put in some sort of gameloop

}