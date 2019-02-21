using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScripts : MonoBehaviour {

	public GameObject obj;
	GameObject player;
	GameObject[] Knifes; 
	float spawnTime;

	// Use this for initialization
	void Start () {
		Instantiate(obj, new Vector3 (0, 0, 0), Quaternion.identity);
		spawnTime = 0.2f;
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		spawnTime -= Time.deltaTime;
		
		Knifes = GameObject.FindGameObjectsWithTag("missile");
		if (spawnTime < 0.0f)
		{

			Instantiate(obj, new Vector3(0, 0, 0), Quaternion.EulerAngles(0, 0, Random.Range(0, 360)));
			spawnTime = 0.2f;

			//new Vector3(-2.3f, 0, 0)
		}

		foreach(GameObject item in Knifes)
		{
			// 맞을 경우 쥬김
			if (item.GetComponent<KnifeMovement>().HeatPlayer())
			{
				player.GetComponent<PlayerMovement>().isHeat = true;
				item.GetComponent<KnifeMovement>().duration = 0.0f;
			}
		}
	}
}
