using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	public GameObject menu;
	public int mainMenu = 2;

    // Start is called before the first frame update
    void Start()
    {
		menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (menu.activeInHierarchy) {
				unpauseFunc();
			} else {
				pauseFunc();
			}
		}
    }

	public void unpauseFunc() {
		menu.SetActive(false);
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Player.instance.isPaused = false;
	}

	public void pauseFunc() {
		menu.SetActive(true);
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Player.instance.isPaused = true;
	}

	public void onContinue() {
		unpauseFunc();
	}

	public void onQuit() {
		Time.timeScale = 1;
		BackgroundScene.instance.goMainMenu(gameObject.scene, mainMenu);
	}

}
