using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{
    public string scene;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GoToScene(scene);
        }
    }

    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
