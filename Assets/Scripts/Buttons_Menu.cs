using UnityEngine;
using UnityEngine.SceneManagement; // Obrigatório para carregar cenas

public class MainMenu : MonoBehaviour
{
   
    public void Jogar()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   
    }

  
    public void SairDoJogo()
    {
        Debug.Log("O jogo foi fechado!"); 
        Application.Quit(); 
    }
}