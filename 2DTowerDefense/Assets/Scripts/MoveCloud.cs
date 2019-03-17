using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    bool _toRight;
    Vector2 startPos;

    int _speed;
    int _dir;
    // Start is called before the first frame update
    void Start()
    {
        _dir = 1;
        _speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * _dir * _speed * Time.deltaTime);

        if (transform.position.x < -15|| 15 < transform.position.x)
        {
            // 가장자리로 가고 속도값 재정의
            _dir *= -1;
            startPos = new Vector2(-9f * _dir, transform.position.y);
            transform.position = startPos;
            _speed = Random.Range(2, 10);
        }
    }
}
