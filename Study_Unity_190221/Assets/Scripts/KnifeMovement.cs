using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour {

	GameObject player;
	public float duration;
	float range;
	Vector3 distance;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		duration = 1.5f;
		transform.Translate(-10f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

		// 이동
		transform.Translate(0.2f, 0, 0);
		duration -= Time.deltaTime;
		// 소멸 조건 // 시간초 체크
		if (duration < 0.0f)
		{
			Destroy(gameObject);
		}

		//distance = gameObject.transform.position - player.transform.position;
		//float dir = distance.magnitude;
		//range = player.GetComponent<PlayerMovement>().range + 0.2f;

		
		//if (dir < range)
		//{

		//	Destroy(gameObject);
		//}
		//else if (gameObject.transform.position())
		//{
		//}
	}

	public bool HeatPlayer()
	{
		distance = gameObject.transform.position - player.transform.position;
		float dir = distance.magnitude;
		range = player.GetComponent<PlayerMovement>().range + 0.2f;

		if (dir < range)
		{
			return true;
		}
		return false;
	}
}
