using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_functionality : MonoBehaviour {
    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp() {
        Application.Quit();
        Debug.Log("Owo, Pwwogram qwit");
    }
}
