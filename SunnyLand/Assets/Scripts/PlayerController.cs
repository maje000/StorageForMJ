using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AniState
{
	AS_IDLE = 0,
	AS_RUN,
}


public class PlayerController : MonoBehaviour {

	bool _IsLiving;
	Rigidbody2D rigid2D;
	Animator animator;
	int speed;
	int jumpForce;

	// Use this for initialization
	void Start () {
		rigid2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		_IsLiving = true;
		speed = 7;
		jumpForce = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_IsLiving)
		{

		}
		else
		{
			float dirX = 0;

			if (Input.GetKey(KeyCode.RightArrow))
			{
				dirX = 1;
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				dirX = -1;
			}

			if (Input.GetKeyDown(KeyCode.Z))
			{
				rigid2D.AddForce(Vector2.up * jumpForce);
			}



			if (dirX != 0)
			{
				transform.localScale = new Vector2(dirX * 5, 5);
				animator.SetInteger("aniState", (int)AniState.AS_RUN);
			}
			else
			{
				animator.SetInteger("aniState", (int)AniState.AS_IDLE);
			}

			transform.Translate(Vector2.right * dirX * speed * Time.deltaTime);
		}
	}
}
