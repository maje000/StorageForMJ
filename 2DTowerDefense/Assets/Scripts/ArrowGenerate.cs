using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerate : MonoBehaviour
{
    public GameObject pref_Arrow;
    float _distance;
    float _speed;
    float _cycleTime;
    bool _isSuper;

    // Start is called before the first frame update
    void Start()
    {
        _speed = 500.0f;
        _distance = 0.1f;
        _cycleTime = 0.0f;
        _isSuper = true;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }//void Update()

    void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            if (!_isSuper)
            {
                GameObject arrow = Instantiate(pref_Arrow);
                arrow.transform.parent = this.transform.parent.parent;
                arrow.transform.position = this.transform.position;// + this.transform.right * _distance;
                arrow.transform.rotation = this.transform.rotation;
                arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * _speed);
            }
            else
            {
                _cycleTime -= Time.deltaTime;
                if (_cycleTime < 0.0f)
                {
                    GameObject arrow = Instantiate(pref_Arrow);
                    arrow.transform.parent = this.transform.parent.parent;
                    arrow.transform.position = this.transform.position; //+ this.transform.right * _distance;
                    arrow.transform.rotation = this.transform.rotation;
                    arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * _speed);
                    _cycleTime = 0.5f;
                }
            }
        }//if (Input.GetMouseButton(0))
    }//void Fire()
}
