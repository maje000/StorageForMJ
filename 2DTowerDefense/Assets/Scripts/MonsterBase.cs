using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBase : MonoBehaviour
{
    public Canvas _canvas;
    Image _HPbar;

    int _curHP, _maxHP; // 현재 체력과 최대체력
    int _speed, _dir; // 속도와 방향
    bool _isArrive;

    protected virtual void Init() {
        _isArrive = false;
        Canvas canvas = Instantiate(_canvas);
        canvas.transform.parent = this.transform;
        canvas.transform.position = transform.position;
        _HPbar = canvas.transform.Find("bar").GetComponent<Image>();
    }
    protected virtual void DoAction()
    {
        if (_curHP < 0) Destroy(gameObject);
    }

    protected int MaxHP
    {
        get { return this._maxHP; }
        set { this._maxHP = value; }
    }

    protected int CurHP
    {
        get { return this._curHP; }
        set { this._curHP = value; }
    }

    protected int Speed
    {
        get { return this._speed; }
        set { this._speed = value; }
    }

    protected int Dir
    {
        get { return this._dir; }
        set { this._dir = value; }
    }

    protected bool IsArrive
    {
        get { return this._isArrive; }
        set { this._isArrive = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!IsArrive && collision.transform.tag == "Castle")
        {
            IsArrive = true;
            Debug.Log("Castle와 충돌");
        }

       

        if (collision.transform.tag == "Arrow")
        {
            CurHP -= 30;
            _HPbar.fillAmount = (float)_curHP / _maxHP;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.transform.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
    }
}
