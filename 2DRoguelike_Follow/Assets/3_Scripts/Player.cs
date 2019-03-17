using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObejct
{
    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;

    public void LoseFood(int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
        CheckIfGameOver();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;
        base.Start();
    }

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        // 이동을 시도하고
        base.AttempMove<T>(xDir, yDir);
        // 음식이 깍이며
        food--;
        // 현재 게임 오버인지 체크
        CheckIfGameOver();
        // 플레이어 턴 off
        GameManager.instance.playerTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        //throw new System.NotImplementedException();
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
            GameManager.instance.GameOver();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Exit":
                {
                    Invoke("Restart", restartLevelDelay);
                    enabled = false;
                }
                break;
            case "Food":
                {
                    food += pointsPerFood;
                    collision.gameObject.SetActive(false);
                }
                break;
            case "Soda":
                {
                    food += pointsPerSoda;
                    collision.gameObject.SetActive(false);
                }
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.instance.playerTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0) // 대각선 이동 방지
            vertical = 0;
        
        if (horizontal != 0 || vertical != 0)
        {
            AttempMove<Wall>(horizontal, vertical);
        }
    }
}
