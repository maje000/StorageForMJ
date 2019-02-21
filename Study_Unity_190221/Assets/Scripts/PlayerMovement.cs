using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	GameObject heart_1;
	GameObject heart_2;
	GameObject heart_3;


	float speed;
	public float range;
	int maxHP;
	int curHP;
	public bool isHeat;



	void Start ()
	{
		speed = 0.3f;
		range = 0.2f;

		curHP = maxHP = 3;
		heart_1 = GameObject.Find("HP_1");
		heart_2 = GameObject.Find("HP_2");
		heart_3 = GameObject.Find("HP_3");
		isHeat = false;
	}
	
	void Update ()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(0, speed, 0);
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(0, -speed, 0);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(speed, 0, 0);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(-speed, 0, 0);
		}

		// 만약 맞으면
		if (isHeat)
		{
			Debug.Log("맞음");
			curHP--;

			// 게임 오버시 소멸
			if (curHP == 0)
			{
				Destroy(gameObject);
			}

			isHeat = false;
		}
	}
}
