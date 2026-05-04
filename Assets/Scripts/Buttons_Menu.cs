using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons_Menu : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
    void SairJogo()
    {
    Application.Quit();
    }

}