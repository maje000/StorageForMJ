using System;
using Random = UnityEngine.Random; // Random은 System과 Unity 둘다 가지고 있음 요건 유니티 엔진꺼임
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Serializable] // 정규화 (인스펙터에서 사용하기 위함)
    public class Count
    {
        public int minimum, maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    // 맵 사이즈 조정
    public int columns = 8;
    public int rows = 8;

    // 벽과 음식의 개수 카운트
    public Count _wallCount = new Count(5, 9);
    public Count _foodCount = new Count(1, 5);

    // prefab으로 받아올 objs
    public GameObject _exit;
    public GameObject[] _floorTiles;
    public GameObject[] _wallTiles;
    public GameObject[] _foodTiles;
    public GameObject[] _enemyTiles;
    public GameObject[] _outerWallTiles;

    // 생성되는 타일들을 한 곳에 모아넣기 위한 곳
    private Transform _boardHolder;

    // 각 타일의 위치값을 List로 저장
    private List<Vector3> _gridPositions = new List<Vector3>();

    // gridPositions 의 초기화를 위한 함수
    void InitializeList()
    {
        _gridPositions.Clear();

       
        for (int x = 1; x < columns - 1; x++) // 머리
        {
            for (int y = 1; y < rows - 1; y++) // 꼬리
            {
                // 위치값 추가
                _gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }


    // Board 설치
    void BoardSetup()
    {
        _boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate;

                // 가생이의 경우 OutWallTile을
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = _outerWallTiles[Random.Range(0, _outerWallTiles.Length)];
                // 안쪽의 경우 FloorTile을
                else
                    toInstantiate = _floorTiles[Random.Range(0, _floorTiles.Length)];

                // 생산해서 각 좌표에 배치하고
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                // _boardHolder에 집어 넣어서 정리해준다.
                instance.transform.SetParent(_boardHolder);
            }
        }
    }
    
    // _gridPisition 안의 랜덤한 위치의 좌표를 잡아온다.
    // 이를 _girdPositions 안에서 없애주며 반환
    Vector3 RandomPosition()
    {

        int randomIndex = Random.Range(0, _gridPositions.Count);
        Vector3 randomPosition = _gridPositions[randomIndex];
        _gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetUpScene(int level)
    {
        BoardSetup();
        InitializeList();

        LayoutObjectAtRandom(_wallTiles, _wallCount.minimum, _wallCount.maximum);
        LayoutObjectAtRandom(_foodTiles, _foodCount.minimum, _foodCount.maximum);

        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(_enemyTiles, enemyCount, enemyCount);

        Instantiate(_exit, new Vector3(columns - 1, rows - 1, 0f),
            Quaternion.identity);
    }
}
