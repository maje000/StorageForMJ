using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // 위아래로 움직이게끔 해주고싶음
    

    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTrigger");
        if (collision.tag == "Player")
        {
            Debug.Log("MeetPlayer");
            Destroy(this.gameObject);
            Debug.Log("DestroyObject");
        }
    }
}
