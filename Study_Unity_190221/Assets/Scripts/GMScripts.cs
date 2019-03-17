///<summary>
///GameState == WAIT;
///게임 시작
///GameState == PLAY;
///캐릭터 소환
///무기 스폰
///캐릭터가 죽으면 
///GameState == DEAD;
///시간초를 버티면
///GameState == CLEAR;
///
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum GameState
{
    GS_WAIT = 0,
    GS_PLAY,
    GS_DEAD,
    GS_CLEAR,
    GS_EXIT,
}

public class GMScripts : MonoBehaviour
{
    public GameObject obj; // Prefab을 받기 위한 임시저장 변수

    GameState _gameState;
    public GameObject _objResultShow;

    GameObject _objCountTime;
    GameObject _objPlayer;
    GameObject[] _HP;
    List<GameObject> _Knifes; // 투사체들의 집합
    float _countTime;
    float _spawnTime;


    // Use this for initialization
    void Start()
    {
        _objCountTime = GameObject.Find("countTime");
        _objPlayer = GameObject.Find("Player");
        _Knifes = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            //_HP[i] = Instantiate("img_HP", new Vector3(),Quaternion.identity);
            //instan
        }
        _gameState = GameState.GS_WAIT;
        _spawnTime = 0.0f;
        _countTime = 30.0f; // 남은 시간
    }

    // Update is called once per frame
    void Update()
    {
        switch(_gameState)
        {
            case GameState.GS_WAIT: // WAIT 대기
                // 특정 이벤트를 통해 PLAY로 이동
                // CLEAR에서 올 경우 다음 Level로
                // DEAD에서 올 경우 초기 Level로
                EscapeWait();
                break;
            case GameState.GS_PLAY: // PLAY 실행
                // 3초간 대기 후 캐릭터 생성 
                // 그리고 Level에 맞게 게임 진행
                // 30초를 버티면 CLEAR로 이동
                // 체력이 다해 죽을 경우 DEAD 이동
                EscapePlay();
                break;
            case GameState.GS_CLEAR: // CLEAR 성공
                // 성공 이벤트 출력
                // 다음 Stage로 이동하기 위해 WAIT으로 감
                EscapeClear();
                break;
            case GameState.GS_DEAD: // DEAD 죽음
                // 죽음 이벤트 출력
                // WAIT으로 이동 or 게임 종료
                EscapeFail();
                break;
            case GameState.GS_EXIT:
                return;
        }
    }

    void EscapeWait()
    {
        // 맞는 조건에 해당하는 상태로 이동
        // Play
        // _gameState = GameState.GS_PLAY;


        // 대기화면 출력

        #region GameState
        // 일단은 Space 입력시 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _objPlayer.GetComponent<PlayerMovement>().enabled = true;
            _objPlayer.GetComponent<PlayerMovement>().CurHP = 3;
            _gameState = GameState.GS_PLAY;
        }
        #endregion GameState
    }

    void EscapePlay()
    {
        // 맞는 조건에 해당하는 상태로 이동
        // CLEAR
        // _gameState = GameState.GS_CLEAR;
        // DEAD
        // _gameState = GameState.GS_DEAD;

        // 일단 진행
        _spawnTime -= Time.deltaTime;
        _countTime -= Time.deltaTime;
        //string str = string.Format("남은 시간 : {0}", _countTime);
        string str;
        Text objCountTimeTxt = _objCountTime.GetComponent<Text>();

        objCountTimeTxt.text = str = string.Format("남은 시간 : {0}", (int)_countTime);
        if (_spawnTime < 0.0f)
        {

            GameObject temp_Knife = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);

            temp_Knife.transform.Rotate(0, 0, Random.Range(0, 360));
            temp_Knife.transform.Translate(-10, 0, 0);
            Debug.Log(str);

            _Knifes.Add(temp_Knife);

            _spawnTime = 1.0f;
        }

        foreach (GameObject item in _Knifes)
        {
            // 먼저 Knife의 지속시간 초과시 제거
            if (item == null)
            {
                _Knifes.Remove(item);
                break;
            }

            // 그다음 체크
            if (CheckHeat(_objPlayer, item))
            {
                // 해당 item 을 리스트에서 빼주고.
                Debug.Log(_Knifes.LastIndexOf(item));
                _Knifes.Remove(item);
                break;
            }
        }


        #region GameState
        if (_objPlayer.GetComponent<PlayerMovement>().CurHP == 0) // 죽었을 경우
        {
            _objPlayer.GetComponent<PlayerMovement>().enabled = false;
            
           GameObject resultObj = Instantiate(_objResultShow, new Vector3(0, 0, 0), Quaternion.identity);
            _objResultShow.SetActive(false);
            _gameState = GameState.GS_DEAD;
        }
        else if (_countTime < 0) // 살아남을 경우
            _gameState = GameState.GS_CLEAR;
        # endregion GameState
    }

    void EscapeClear()
    {
        // 맞는 조건에 해당하는 상태로 이동
        // WAIT
        #region GameState
        _gameState = GameState.GS_WAIT;
        # endregion GameState
    }

    void EscapeFail()
    {
        // 맞는 조건에 해당하는 상태로 이동
        #region GameState
        // WAIT
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(GameObject.Find("ResultShow"));
            _gameState = GameState.GS_WAIT;
        }
        // EXIT
        //_gameState = GameState.GS_EXIT;
        # endregion GameState
    }

    bool CheckHeat(GameObject Player, GameObject Knife)
    {
        // 거리 측정하고
        Vector3 DistanceVec = Player.transform.position - Knife.transform.position;
        float Distance = DistanceVec.magnitude;
        // 반경 측정하고
        float radious = Player.GetComponent<PlayerMovement>().Radious + Knife.GetComponent<KnifeMovement>().Radious;

        // 거리가 반경의 합보다 적으면 충돌
        if (Distance < radious)
        {
            // 부딪힘
            _objPlayer.GetComponent<PlayerMovement>().IsHeat = true;
            Knife.GetComponent<KnifeMovement>().IsHeat = true;
            return true;
        }

        // 안부딪힘
        return false;
    }
}
