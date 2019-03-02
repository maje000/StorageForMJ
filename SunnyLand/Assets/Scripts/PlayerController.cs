using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AniState
{
	AS_IDLE = 0,
	AS_RUN,
}


public class PlayerController : MonoBehaviour {

    // 변수 선언
	bool _IsLiving; // 캐릭터가 죽으면 움직일 수 없게 하기 위한 변수
	Rigidbody2D rigid2D; // 물리 운동시켜줄려궁
	Animator animator; // 상황에 따른 애니메이션변경
	int speed; // 객체 이동 속도 translate()
	int jumpForce; // 점프했을대 얻을 힘 AddForce()
    float MaxSpeed;

	// Use this for initialization
	void Start () {
		rigid2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		_IsLiving = true;
		speed = 7;
		jumpForce = 700;
        MaxSpeed = 700;

    }
	
	// Update is called once per frame
	void Update () {
		if (!_IsLiving)
		{
            Debug.Log("죽음");
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
                animator.SetInteger("", 2);
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

            //rigid2D.AddForce(Vector2.right * dirX * speed * Time.deltaTime);

			transform.Translate(Vector2.right * dirX * speed * Time.deltaTime);
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision.gameObject.name + "랑 충돌");
            Vector3 vec = transform.position - collision.transform.position;
            rigid2D.AddForce(vec.normalized * jumpForce);
            Debug.Log(vec.normalized * jumpForce);
        }
    }
}
