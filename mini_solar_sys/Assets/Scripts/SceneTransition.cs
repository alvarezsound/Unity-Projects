using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SolarSystem");
    }
}
