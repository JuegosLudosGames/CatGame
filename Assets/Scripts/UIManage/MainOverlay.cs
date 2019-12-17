using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainOverlay : MonoBehaviour
{

	public static MainOverlay instance;

	public Text Timer;
	public Text CoinTotal;
	public Text CoinPer;

	public GameObject playerObj;

	int coinTotal = 0;

	float time = 0;

	public bool isPaused = false;

	// Start is called before the first frame update
	void Start()
    {
		instance = this;
		GameObject.Instantiate(BackgroundScene.instance.loadedPrefab, Player.instance.gameObject.transform);
		Player.instance.reloadObjects();
    }

    // Update is called once per frame
    void Update()
    {
		if (!isPaused && Player.instance.canPlay) {
			time += Time.deltaTime;

			Debug.Log("Total " + coinTotal + " Time " + time);

			Debug.Log("is paused " + isPaused + " canPlay " + Player.instance.canPlay);

			//update visuals
			Timer.text = Mathf.Floor((time % 3600)/60).ToString("00") + ":" + (time % 60).ToString("00");
			CoinPer.text = Math.Round((Decimal)((float)coinTotal / (time / 60)), 2, MidpointRounding.ToEven).ToString();
		}
    }

	public void CollectCoin() {
		coinTotal++;
		CoinTotal.text = coinTotal.ToString();
	}

}
