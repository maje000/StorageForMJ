using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	int _maxHP; // 최대체력
	int _curHP; // 현재체력
	float _speed; // 이동속도
	float _radious; // 충돌반경
	bool _isHeat; // 충돌체크



	void Start ()
	{
		_speed = 0.3f;
        _radious = 0.2f;
		_curHP = _maxHP = 3;
		_isHeat = false;
	}
	
	void Update ()
	{
        // 캐릭터 조작
        HandleCharacter();

        // 만약 맞으면
        CheckDamage();
    }

    void HandleCharacter()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, _speed, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -_speed, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(_speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-_speed, 0, 0);
        }
    }

    void CheckDamage()
    {
        if (_isHeat)
        {
            Debug.Log("맞음");
            _curHP--;

            // 게임 오버시 소멸
            if (_curHP == 0)
            {
                Debug.Log("죽음");
            }

            _isHeat = false;
        }
    }

    #region property
    public bool IsHeat
    {
        //get { return _isHeat; }
        set { this._isHeat = value; }
    }

    public int CurHP
    {
        get { return this._curHP; }
        set { this._curHP = value; }
    }

    public Vector3 CurPos
    {
        get { return transform.position; }
        set { transform.SetPositionAndRotation(value, Quaternion.identity); }
    }

    public float Radious
    {
        get { return _radious; }
        //set { this._radious = value; }
    }
    #endregion property
}
