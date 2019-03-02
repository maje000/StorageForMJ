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
public class MonsterController : MonoBehaviour
{
    MonsterState _monsterState;
    Animator _animator;
    Rigidbody2D _rigid2D;
    Vector3 _curScale;
    float _cycleTime;
    float _speed;
    float dir;

    bool _isDead;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _speed = 0.0f;
        _cycleTime = 2.0f;
        dir = 1;
        _isDead = true;

        _curScale = transform.localScale;

        if (gameObject.name == "Eagle")
        {
            _monsterState = MonsterState.Eagle;
            _speed = 2.0f;
        }
        else if (gameObject.name == "Frog")
        {
            _monsterState = MonsterState.Frog;
            _speed = 1.0f;
        }
        else if (gameObject.name == "Opossum")
        {
            _monsterState = MonsterState.Opossum;
            _speed = 1.0f;
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
        transform.Translate(tempVec * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 vec = collision.transform.position - transform.position;
        float degree = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        if (degree < 120 && 60 < degree) _animator.SetBool("isDead", true);
    }
}
