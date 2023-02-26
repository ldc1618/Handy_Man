using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_active: MonoBehaviour
{
	public GameObject Pause_menu;

	public static bool isPaused; //pausing varaible
    // Start is called before the first frame update
    void Start()
    {
        Pause_menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape)) 
	   {
			if(isPaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
	   }
    }

	// bring up pause menu
	public void PauseGame()
	{
		Pause_menu.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}


	public void ResumeGame()
	{
		Pause_menu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;

	}
}

