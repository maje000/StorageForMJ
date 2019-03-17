using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Peach : MonsterBase
{
    void Start()
    {
        
        Init();
    }

    void Update()
    {
        
        DoAction();
    }
    protected override void Init()
    {
        base.Init();

        CurHP = MaxHP = 100;
        Speed = 2;
        Dir = 1; // 추후 변경
    }

    protected override void DoAction()
    {
        base.DoAction();
        // Dir = -1 || 1;
        // 그러면 이걸로 연산해주고 싶은데 Dir == 1 은 오른쪽으로 이동할때이고,
        // Dir == -1 은 왼쪽으로 이동할때이니깐
        // 스케일은 -1, 1, 1 || 1, 1, 1; 갭은 2차이 1, 1, 1 에서 그럼  (1 - Dir)을 뺴주면 대나
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
