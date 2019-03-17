using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Transform inOutButton;

    Vector2 _buttonStartPos;
    Vector2 _buttonEndPos;
    bool isOutButton;
    float _InOutspeed;
    int _dir;

    // Start is called before the first frame update
    void Start()
    {
        inOutButton = transform.Find("InOutButton").transform;
        isOutButton = false;
        _dir = -1;
        _InOutspeed = 10;
        _buttonStartPos = new Vector2(-7.75f, 2.3f);
        _buttonEndPos = new Vector2(-9.75f, 2.3f);
    }

    // Update is called once per frame
    void Update()
    {
        UIPosition();
        //Debug.Log(transform.position.x);
    }


    public void OnInOutButton()
    {
        //Debug.Log("클릭");
        isOutButton = !isOutButton;
        //Debug.Log(isOutButton);
        _dir *= -1;
        //Debug.Log(_dir);
    }

    public void OnPlusButton(int num)
    {
        switch(num)
        {
            case 0:
                //AddApple();
                Debug.Log("isAddApple");
                break;
            case 1:
                //AddBow();
                Debug.Log("isAddBow");
                break;
            case 2:
                //LevelUpCastlr();
                Debug.Log("isLevelUpCastlr");
                break;
            default:
                break;
        }
    }

    void UIPosition()
    {
        Vector2 arrive = Vector2.one;
        if (isOutButton) // 그리고 특정 거리 체크
        {
            arrive = _buttonEndPos - (Vector2)transform.position;
        }
        else
        {
            arrive = _buttonStartPos - (Vector2)transform.position;
        }

        //Debug.Log("arrive" + arrive + "_buttonEndpos" + _buttonEndPos + "_buttonStartPos" + _buttonStartPos + "transform.position" + transform.position);
        transform.Translate(arrive * Time.deltaTime * 20/*speedB*/);
    }
}
