using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonsterController
{
    public const float speed = 7.0f;

    void Start()
    {
        MonsterInit(speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
