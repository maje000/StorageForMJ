using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaFuntion : MonoBehaviour
{
    // 상호 관계하는 모든 함수 관리
    public static Vector3 CheckVec(Vector3 vec1, Vector3 vec2)
    {
        return vec2 - vec1;
    }
}
