// fsm 삽입하기

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MonsterState
{
    Type = 0,
    Eagle,
    Frog,
    Opossum,
}

public class MonsterController : BaseActor
{
    MonsterState _monsterState;

    float _cycleTime;
    Vector3 _curScale;
    float dir;


    // Start is called before the first frame update
    void Start()
    {
        MonsterInit(15.0f);
        _cycleTime = 2.0f;
        dir = 1;

        _curScale = transform.localScale;

        if (gameObject.transform.tag == "Eagle")
        {
            _monsterState = MonsterState.Eagle;
            SetSpeed = 2.0f;
        }
        else if (gameObject.transform.tag == "Frog")
        {
            _monsterState = MonsterState.Frog;
            SetSpeed = 1.0f;
        }
        else if (gameObject.transform.tag == "Opossum")
        {
            _monsterState = MonsterState.Opossum;
            SetSpeed = 1.0f;
        }

        Debug.Log(_monsterState + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        _cycleTime -= Time.deltaTime;
        DoAction(_monsterState);
    }

    void DoAction(MonsterState state)
    {
        Vector2 tempVec = new Vector2();
        switch(state)
        {
            case MonsterState.Eagle:
                if (_cycleTime < 0.0f)
                {
                    _cycleTime = 2.0f;
                    
                    dir *= -1;
                }
                tempVec = Vector2.up * dir;
                break;
            case MonsterState.Frog:
                if (_cycleTime < 0.0f)
                {
                    _cycleTime = 2.0f;
                    dir *= -1;
                }
                tempVec = Vector2.left * dir;
                break;
            case MonsterState.Opossum:
                if (_cycleTime < 0.0f)
                {
                    _cycleTime = 2.0f;
                    dir *= -1;
                }
                tempVec = Vector2.left * dir;
                break;
            default:
                Debug.Log("out of range in MonsterState");
                break;
        }

        Vector3 tempScale = new Vector3(_curScale.x * dir, _curScale.y, _curScale.z);
        transform.localScale = tempScale;
        transform.Translate(tempVec * SetSpeed * Time.deltaTime);
    }

    protected void MonsterInit(float speed)
    {
        SetAni = GetComponent<Animator>();
        SetRigid2D = GetComponent<Rigidbody2D>();

        SetDeath = false;
        SetSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            SetAni.SetBool("isDead", MetaFuntion.IsHit(this.transform, collision.transform));
        }
    }
}
