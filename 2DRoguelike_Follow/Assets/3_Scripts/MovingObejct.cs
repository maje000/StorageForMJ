using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObejct : MonoBehaviour
{
    public float moveTime = .1f;
    public LayerMask blockingLayer;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rigid2D;
    private float inverseMoveTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigid2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position; // 자기 위치를 시작지점으로 하고
        Vector2 end = start + new Vector2(xDir, yDir); // 받아온 값을 이동하는 곳으로

        boxCollider.enabled = false; // 우선 BoxCollider를 지우고
        hit = Physics2D.Linecast(start, end, blockingLayer); // Linecast를 쏴서 부딪히는 걸 체크
        boxCollider.enabled = true; // BoxCollider를 다시 씌움

        if (hit.transform == null) // 만약 부딪히는 것이 없으면
        {
            StartCoroutine(SmoothMovement(end)); // 코루틴 실행
            return true;
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        // 거리의 제곱을 구한뒤
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        // 거리의 제곱이 0에 한없이 가까울 경우 종료
        while(sqrRemainingDistance > float.Epsilon)
        {
            // 현재의 위치와 이동 위치를 inverseMoveTime * Time.deltaTime으로 이동하는 좌표 만들고
            Vector3 newPosition = Vector3.MoveTowards(rigid2D.position, end, inverseMoveTime * Time.deltaTime );

            // 그 좌표로 이동 시켜주며
            rigid2D.MovePosition(newPosition);

            // 거리를 재설정
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
    }

    protected virtual void AttempMove<T>(int xDir, int yDir)
        where T : Component
    {
        // RaycastHit2D 지정
        RaycastHit2D hit;
        // 움직임 체크
        bool canMove = Move(xDir, yDir, out hit);

        // 만약 부딪힌게 없을 경우 return
        if (hit.transform == null)
            return;

        // 부딪힌 물체를 가져와서
        T hitComponent = hit.transform.GetComponent<T>();

        // canMove == false == 이동 방향에 물체가 잇고
        // HhitCComponent != null == 이동 방향에 무엇인가가 있을 경우
        if (!canMove && hitComponent != null)
            // OnCantmove 실행
            OnCantMove(hitComponent);
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;

    

}
