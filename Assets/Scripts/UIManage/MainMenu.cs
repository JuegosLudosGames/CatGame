using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public int SplashScreenIndex = 0;
	public GameObject main;
	public GameObject charSel;
	public int MainSelect = 3;

	public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
		goToMain();
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	public void goToMain() {
		charSel.SetActive(false);
		main.SetActive(true);
	}

	public void enterCharSelection() {
		charSel.SetActive(true);
		main.SetActive(false);
	}

	public void selectChar(int a) {
		BackgroundScene.instance.loadScene(MainSelect, prefabs[a], gameObject.scene);
	}

	public void quit() {
		Application.Quit();
	}

	private void LateUpdate() {
		if ((!BackgroundScene.instance.SplashUnloaded) && BackgroundScene.instance.clearUnloadSplash) {
			BackgroundScene.instance.SplashUnloaded = true;
			SceneManager.UnloadSceneAsync(SplashScreenIndex);
		}
	}
}
