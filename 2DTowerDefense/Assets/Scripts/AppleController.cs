using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    Transform _sprite;
    public GameObject _weapon;

    int _weaponCount;
    float _distance = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = transform.Find("Sprite");
        _weaponCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSprite();
        if (Input.GetKeyDown(KeyCode.A))
            AddWeapon();
    }

    // Sprite의 좌우 반전을 위한
    void CheckSprite()
    {
        float angle = GetAngleToMouse();

        if (90 < angle && angle < 270)
        {
            _sprite.GetComponentInChildren<SpriteRenderer>().flipY = true;
        }
        else
        {
            _sprite.GetComponentInChildren<SpriteRenderer>().flipY = false;
        }

        _sprite.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }

    float GetAngleToMouse() // 마우스까지의 각도 얻기 (0도 ~ 360도)
    {
        // 현재 마우스 포인트의 위치를 position을 중심으로한 Localposition으로 치환
        Vector3 curMousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localMousePoint = curMousePoint - transform.position;

        float angle = Vector2.Angle(Vector2.right, localMousePoint);

        // Quaternion의 값은 0 ~ 180이기에 180 ~ 360의 각도를 만들기 위해
        // angle 값이 180을 넘어가는 기준을 잡아서 수정
        if (localMousePoint.y < 0) angle = 360 - angle;

        return angle;
    }

    // 무기 추가
    public void AddWeapon()
    {
        if (_weaponCount < 3)
        {
            // 무기 생성
            GameObject weapon = Instantiate(_weapon);

            // 무기의 부모를 생성 객체의 자식으로
            weapon.transform.parent = this.transform.Find("Sprite");

            // 무기 생성을 위한 좌표 수정
            float fAngle = GetAngleToMouse();
            weapon.transform.rotation = Quaternion.AngleAxis(fAngle, Vector3.forward);

            weapon.transform.position = transform.position + (Vector3.right * _distance * _weaponCount);
            _weaponCount++;
        }
        else
        {
            Debug.Log("한계초과");
        }
    }
}
