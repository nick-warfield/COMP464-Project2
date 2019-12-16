using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
	public GameObject MainMenu, SettingsMenu;

	public void start()
	{
		SceneManager.LoadScene("Game Mode");
	}

	public void build()
	{
		SceneManager.LoadScene("Build Mode");
	}

	public void quit()
	{
		Application.Quit();
	}

	public void settings()
	{
		SettingsMenu.SetActive(true);
		MainMenu.SetActive(false);
	}

	public void back()
	{
		MainMenu.SetActive(true);
		SettingsMenu.SetActive(false);
	}
}
