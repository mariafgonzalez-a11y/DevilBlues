using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons_Menu : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }
    public void SairJogo()
    {
    Application.Quit();
    }

}