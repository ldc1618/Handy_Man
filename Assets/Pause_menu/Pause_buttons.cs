using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_buttons : MonoBehaviour {
    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp() {
        Application.Quit();
        Debug.Log("Owo, Pwwogram qwit");
    }
}
