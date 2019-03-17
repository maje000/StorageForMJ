using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseActor : MonoBehaviour
{
    // Component Socket
    Animator _animator;
    Rigidbody2D _rigid2D;

    // 공통 인자
    float       _speed = 0;
    bool        _isDead = false;

    // 이건 Override로 넘겨주고
    public virtual void DoAction() { }


    // 애니메이션
    protected Animator SetAni
    {
        get { return this._animator; }
        set { this._animator = value; }
    }

    // Rigidbody
    protected Rigidbody2D SetRigid2D
    {
        get { return this._rigid2D; }
        set { this._rigid2D = value; }
    }

    // 생사여부
    protected bool SetDeath
    {
        get { return this._isDead; }
        set { this._isDead = value; }
    }

    // 객체속도
    protected float SetSpeed
    {
        get { return this._speed; }
        set { this._speed = value; }
    }
}
