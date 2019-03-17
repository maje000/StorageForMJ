using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Potato : MonsterBase
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        DoAction();
    }

    protected override void Init()
    {
        base.Init();

        CurHP = MaxHP = 1000;
        Speed = 1;
        Dir = 1; // 추후 변경
    }

    protected override void DoAction()
    {
        base.DoAction();

        if (IsArrive)
        {
            //Debug.Log("도착");
        }
        else
        {
            transform.localScale = Vector3.one - Vector3.right * (1 - Dir);
            transform.Translate(Vector2.left * Dir * Speed * Time.deltaTime);
        }
    }
}
