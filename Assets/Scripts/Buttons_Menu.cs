using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons_Menu : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    void Jogar()
    {

        SceneManager.LoadScene(Fase1);
    }
    void SairJogo()
    {
    Application.Quit();
    }

}