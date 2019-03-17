/*
 타겟을 정해서 x값의 변동에 따라서 움직이게끔
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 타겟 설정
    public Transform target;

    // z값 고정
    float _offsetZ;

    void Start()
    {
        // z값 고정을 위한 변수초기화
        _offsetZ = -10;
        // 처음 위치는 target으로
        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 위치를 실시간으로 대입해서 위치 이동
        transform.position = new Vector3(target.position.x, target.position.y, _offsetZ);
    }
}
