using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void ExitGame()
   {
      Application.Quit();
   }

   public void StartGame()
   {
      SceneManager.LoadScene(1); 
   }

   public void ReturnToMenu()
   {
      SceneManager.LoadScene(0);
   }
}
