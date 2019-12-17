using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{

	public int backgroundSceneId;
	public int MainMenuId;
	public float fadeTime;
	public MaskableGraphic toFade;
	bool isFading = true;
	Color t;
	bool isCheckingFinished = false;
	public GameObject completeMsg;
	public GameObject loadInd;
	Scene finishedLoad;
	float curTime = 0;

    // Start is called before the first frame update
    void Start()
    {
		t = toFade.color;
		isFading = true;
		completeMsg.SetActive(false);
		loadInd.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
		if (isFading) {
			curTime += Time.deltaTime;
			toFade.color = new Color(t.r, t.g, t.b, (curTime/fadeTime) * 2.55f);
			if (toFade.color.a >= 1) {
				isFading = false;
				SceneManager.sceneLoaded += onBackgroundComplete;
				SceneManager.LoadSceneAsync(backgroundSceneId, LoadSceneMode.Additive);
			}
		} else if (isCheckingFinished) {
			if (Input.anyKeyDown) {
				SceneManager.SetActiveScene(finishedLoad);
				BackgroundScene.instance.clearUnloadSplash = true;
			}
		}
    }

	void onBackgroundComplete(Scene s, LoadSceneMode e) {
		SceneManager.sceneLoaded -= onBackgroundComplete;
		SceneManager.sceneLoaded += onMainComplete;
		SceneManager.LoadSceneAsync(MainMenuId, LoadSceneMode.Additive);
	}

	void onMainComplete(Scene s, LoadSceneMode e) {
		completeMsg.SetActive(true);
		loadInd.SetActive(false);
		SceneManager.sceneLoaded -= onMainComplete;
		isCheckingFinished = true;
		finishedLoad = s;
	}

}
