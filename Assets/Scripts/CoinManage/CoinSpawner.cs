using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

	public static CoinSpawner instance;

	public Transform[] spawnPoints;
	public GameObject coinPrefab;
	public Transform parent;
	public Transform camera;
	public int prev = -1;

	Random rnd;

	// Start is called before the first frame update
	void Start()
    {
		instance = this;
		rnd = new Random();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void spawnCoin() {
		int i = prev;
		while (i == prev) {
			i = new System.Random().Next(spawnPoints.Length);
		}
		prev = i;
		Vector3 position = spawnPoints[i].position;

		GameObject o = GameObject.Instantiate(coinPrefab, position, new Quaternion(0, 0, 0, 0), parent);

		o.GetComponent<Coin>().cam = camera;
	}

	public void onCoinPickup() {
		spawnCoin();
	}
}
