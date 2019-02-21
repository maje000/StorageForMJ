using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour {

	float _duration; // 지속시간
    float _speed;
	float _radious; // 충돌반경
    bool _isHeat; // 충돌체크

	// Use this for initialization
	void Start () {
        _duration = 1.5f;
        _speed = 0.2f;
        _radious = 0.2f;
        _isHeat = false;
	}
	
	// Update is called once per frame
	void Update () {
        // 지속시간 감소
		_duration -= Time.deltaTime;

		// 이동
		transform.Translate(_speed, 0, 0);

		// 소멸 조건 // 지속시간 체크 or 충돌
		if (_duration < 0.0f || _isHeat)
		{
			Destroy(gameObject);
		}
	}

    #region property
    public bool IsHeat
    {
        //get { return this._isHeat; }
        set { this._isHeat = value; }
    }

    public Vector3 CurPos
    {
        get { return gameObject.transform.position; }
        //set { gameObject.transform.SetPositionAndRotation(value, Quaternion.identity); }
    }

    public float Radious
    {
        get { return _radious; }
        //set { this._radious = value; }
    }
    #endregion property
}
