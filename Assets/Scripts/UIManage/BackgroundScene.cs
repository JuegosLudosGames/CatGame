using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundScene : MonoBehaviour
{

	public static BackgroundScene instance;

	[HideInInspector]
	public bool clearUnloadSplash = false;
	[HideInInspector]
	public bool SplashUnloaded = false;

	[HideInInspector]
	public bool loading = false;
	[HideInInspector]
	public bool loadComplete = false;
	[HideInInspector]
	public bool isMain = false;
	public GameObject loadcompleteShowText;
	public GameObject loadLoadingText;
	public GameObject blackScreen;

	[HideInInspector]
	public GameObject loadedPrefab;

    // Start is called before the first frame update
    void Start()
    {
		instance = this;
		loadLoadingText.SetActive(false);
		loadcompleteShowText.SetActive(false);
		blackScreen.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if (loading && loadComplete) {
			if (Input.anyKeyDown) {
				loadLoadingText.SetActive(false);
				loadcompleteShowText.SetActive(false);
				blackScreen.SetActive(false);
				SceneManager.SetActiveScene(loaded);
				loadComplete = false;
				loading = false;
				if (isMain) {
					isMain = false;
				} else {
					Player.instance.canPlay = true;
				}
			}
		}
    }

	int toload;

	public void loadScene(int id, GameObject pre, Scene cur) {
		loadLoadingText.SetActive(true);
		blackScreen.SetActive(true);
		SceneManager.SetActiveScene(gameObject.scene);
		loadedPrefab = pre;
		loading = true;
		toload = id;
		SceneManager.sceneUnloaded += onUnLoadComplete;
		SceneManager.UnloadSceneAsync(cur);
	}

	void onUnLoadComplete(Scene s) {
		SceneManager.sceneUnloaded -= onUnLoadComplete;
		SceneManager.sceneLoaded += onLoadComplete;
		SceneManager.LoadSceneAsync(toload, LoadSceneMode.Additive);
	}

	Scene loaded;

	void onLoadComplete(Scene s, LoadSceneMode e) {
		loadLoadingText.SetActive(false);
		loadcompleteShowText.SetActive(true);
		loadComplete = true;
		loaded = s;
		SceneManager.sceneLoaded -= onLoadComplete;
	}

	public void goMainMenu(Scene cur, int id) {
		loadLoadingText.SetActive(true);
		blackScreen.SetActive(true);
		SceneManager.SetActiveScene(gameObject.scene);
		loading = true;
		isMain = true;
		toload = id;
		SceneManager.sceneUnloaded += onUnloadMain;
		SceneManager.UnloadSceneAsync(cur);
	}

	void onUnloadMain(Scene s) {
		SceneManager.sceneUnloaded -= onUnloadMain;
		SceneManager.sceneLoaded += onLoadComplete;
		SceneManager.LoadSceneAsync(toload, LoadSceneMode.Additive);
	}



}
