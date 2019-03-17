using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AniState
{
	AS_IDLE = 0,
	AS_RUN = 1,
    AS_JUMPUP = 2,
    AS_FALLDOWN = 3,
    AS_HURT = 4,
}


public class PlayerController : BaseActor
{

    // 변수 선언
    AniState _aniState;
    bool _isHaveit;
    bool _isAir;
    bool _isHeat;

    int _dir;
	int _jumpForce; // 점프했을대 얻을 힘 AddForce()에 사용
    int _BouncingForce;
    //float MaxSpeed;

	// Use this for initialization
	void Start () {
        // Component 셋팅
        SetAni = GetComponent<Animator>();
        SetAni.SetInteger("aniState", (int)AniState.AS_IDLE);
        SetRigid2D = GetComponent<Rigidbody2D>();

        // Parameter 셋팅
        SetDeath = false;
        SetSpeed = 7;

        _dir = 0;
        _isHaveit = false;
        _isAir = false;
        _isHeat = false;
        _jumpForce = 700;
        _BouncingForce = 500;
        //MaxSpeed = 700;

    }
	
	// Update is called once per frame
	void Update () {

        if (SetRigid2D.velocity.y >= 0.1f || SetRigid2D.velocity.y <= -0.1f) _isAir = true;

        // 키 입력 받고
        GetKeybuff();

        // 후 처리
        if (SetDeath)
		{

            Debug.Log("죽음");
		}
		else
		{

            DoAction();
		}
	}

    public override void DoAction()
    {
        switch(_aniState)
        {
            case AniState.AS_IDLE:
                DoIdle();
                break;
            case AniState.AS_RUN:
                DoRun();
                break;
            case AniState.AS_JUMPUP:
                DoJump();
                break;
            case AniState.AS_FALLDOWN:
                DoFalling();
                break;
            case AniState.AS_HURT:
                DoHurt();
                break;
        }

        if (_dir != 0)
        {
            transform.localScale = new Vector2(_dir * 5, 5);
            transform.Translate(Vector2.right * _dir * SetSpeed * Time.deltaTime);
        }
    }

    void GetKeybuff()
    {
        _dir = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _dir = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _dir = -1;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("KeyCode Z");
            if (!(_isAir || _isHeat))
            {
                Debug.Log("AddForce");
                SetRigid2D.AddForce(Vector2.up * _jumpForce);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // Debug용
            ShowState();
        }
    }

    void DoIdle()
    {

        if (_isHeat)
        {
            _aniState = AniState.AS_HURT;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
        else if (_isAir) // 여기는 올꺼같진 않지만 혹시 모르니 ㅎㅎ;;
        {
            _aniState = AniState.AS_JUMPUP;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
        else if (_dir != 0)
        {
            _aniState = AniState.AS_RUN;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
    }

    void DoRun()
    {


        if (_isHeat)
        {
            _aniState = AniState.AS_HURT;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
        else if (_isAir)
        {
            _aniState = AniState.AS_JUMPUP;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
        else if (_dir == 0)
        {
            _aniState = AniState.AS_IDLE;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
    }

    void DoJump()
    {
        _isAir = true;

        if (_isHeat)
        {
            _aniState = AniState.AS_HURT;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
        else if (SetRigid2D.velocity.y <= 0.0f)
        {
            _aniState = AniState.AS_FALLDOWN;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
    }

    void DoFalling()
    {
        if (_isHeat)
        {
            _aniState = AniState.AS_HURT;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
        else if (SetRigid2D.velocity.y >= 0.0f)
        {
            _isAir = false;
            _aniState = AniState.AS_IDLE;
            SetAni.SetInteger("aniState", (int)_aniState);
        }
    }

    void DoHurt()
    {
        // 플레이어의 체력을 깍고 만약 0이면 죽음

        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);

        _isHeat = false;
        _aniState = AniState.AS_IDLE;
        SetAni.SetInteger("aniState", (int)_aniState);
    }

    void ShowState()
    {
        Debug.Log(SetRigid2D.velocity);
        Debug.Log(_isAir);
        //Debug.Log();
        //Debug.Log();
        //Debug.Log();
        //Debug.Log();
        //Debug.Log();
    }

  

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            // 몬스터의 머리 위로 떨어질 경우는 때린거고, 그렇지 않을 경우 맞은것으로 분류
            if (MetaFuntion.IsHit(collision.transform, this.transform))
            {
                Debug.Log(collision.transform.tag + "때림");
                SetRigid2D.AddForce(Vector2.up * _BouncingForce);
            }
            else
            {
                Debug.Log(collision.transform.tag + "에게 맞음");
                // 부딪치면 그 방향으로 튕기게 하기 위해 position으로 방향을 정해서 AddForce로 튕김
                Vector2 vec = this.transform.position - collision.transform.position;
                SetRigid2D.AddForce(vec.normalized * _BouncingForce);
                _isHeat = true;
                //_isLiving = false;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gem")
        {
            SetSpeed += 1;
        }

        if (collision.tag == "Cherry")
        {
            _isHaveit = true;
        }

        if (collision.tag == "Finish")
        {
            if (_isHaveit)
            {
                Debug.Log("Finish!!!!");
            }
        }
    }
}
