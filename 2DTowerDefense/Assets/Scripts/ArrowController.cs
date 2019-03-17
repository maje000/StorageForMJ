using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    float _duration;
    int _dir;
    // Start is called before the first frame update
    void Start()
    {
        _duration = 4.0f;
        _dir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _duration -= Time.deltaTime;
        CheckArrowAngle();

        if (_duration < 0.0f)
        {
            Destroy(this.gameObject);
        }
    }


    void CheckArrowAngle()
    {
        Vector2 vec = transform.GetComponent<Rigidbody2D>().velocity;
        float angle = Vector2.Angle(Vector2.up, vec);
        //Debug.Log(transform.GetComponent<Rigidbody2D>().velocity);

        if (vec.x < 0) _dir = -1;
        else _dir = 1;

        transform.localScale = Vector3.one - (Vector3.right * (1 - _dir));
        transform.rotation = Quaternion.AngleAxis((90 - angle) * _dir, Vector3.forward);
        //if (vec.x < 0)
        //{
        //    dir = -1;
        //    //transform.GetComponent<SpriteRenderer>().flipX = true;
        //    //transform.localScale = Vector3.one + (Vector3.right * -2);
        //    //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //}
        //else
        //{
        //    dir = 1;
        //    //transform.localScale = Vector3.one;
        //    //transform.rotation = Quaternion.AngleAxis(90 - angle, Vector3.forward);
        //}

        //transform.localScale = Vector3.one - (Vector3.right * (1 - _dir));
        //transform.rotation = Quaternion.AngleAxis((90 - angle) * _dir, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Castle")
        {

            Destroy(this.gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag != "Castle")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}
