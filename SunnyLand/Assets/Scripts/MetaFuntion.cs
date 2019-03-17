using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaFuntion : MonoBehaviour
{
    // 상호 관계하는 모든 함수 관리
    public static float CheckDistance(Vector3 vec1, Vector3 vec2) // vec1에서 vec2 로의 거리
    {
        Vector3 temp = vec2 - vec1;

        return temp.magnitude;
    }

    public static bool IsHit(Transform a, Transform b)
    {
        Vector3 vec = b.position - a.position;
        float degree = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        if (degree < 160 && 20 < degree)
            return true;

        return false;
    }
}
