using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerate : MonoBehaviour
{
    enum Monster
    {
        Peach = 0,
        Pear,
        Potato,
    }

    public GameObject[] pref_Monsters;

    float _spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject item in pref_Monsters)
        {
            if (item == null)
            {
                Debug.Log(item.name);
            }
        }

        _spawnTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTime -= Time.deltaTime;

        if (_spawnTime < 0.0f)
        {
            GameObject Monster = Instantiate(pref_Monsters[0]);
            Monster.transform.position = new Vector2(9.5f , -2.8f);
            _spawnTime = 0.5f;
        }
    }
}
