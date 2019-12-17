using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

	public Transform cam;
	public float dis = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		//gets the relative coords of the camera using distance
		Vector3 rel = cam.position - transform.position;
		transform.rotation = Quaternion.LookRotation(rel);

	//	Debug.Log(Vector3.Distance(Player.instance.transform.position, transform.position));

		if (Vector3.Distance(Player.instance.transform.position, transform.position) <= dis) {
			//Debug.Log("yay");
			MainOverlay.instance.CollectCoin();
			CoinSpawner.instance.onCoinPickup();
			GameObject.Destroy(gameObject);
		} //else {
		//	Debug.Log("Nay");
		//}
	}
}
