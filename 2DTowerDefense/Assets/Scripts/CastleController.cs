using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    public Image _hpBar;
    public GameObject[] _castleImage;
    
    float time;
    int _level;
    int _curHP;
    int _maxHP;

    // Start is called before the first frame update
    void Start()
    {

        time = 0;
        _curHP = _maxHP = 500;
        _level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        if (_level == 1)
        {
            // _castleImage[_level-1] 이거 불러서 처리
            _level++;
        }
        else if (_level == 2)
        {
            // _castleImage[_level-1] 이거 불러서 처리
            _level++;
        }
        else
        {
            Debug.Log("만땅임다");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("stay");
        time -= Time.deltaTime;
        if (time < 0)
        {
            _curHP -= 10;
            _hpBar.fillAmount = (float)_curHP / _maxHP;
            time = 2;
        }
    }

    
}
