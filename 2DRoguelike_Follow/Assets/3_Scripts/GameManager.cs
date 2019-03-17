using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public GameObject player;
    private int level = 4;
    //[HideInInspector]
    public int playerFoodPoints = 100;
    [HideInInspector]
    public bool playerTurn = true;

    public float turnDelay = .1f;
    private List<Enemy> enemies;
    private bool enemiesMoving;

    public static GameManager instance = null;

    public void GameOver()
    {
        enabled = false;
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //boardScript = GetComponent<BoardManager>();
        enemies = new List<Enemy>();

        InitGame();
    }

    private void InitGame()
    {
        enemies.Clear();
        boardScript.SetUpScene(level);
        Instantiate(player);
    }

    private IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playerTurn = true;
        enemiesMoving = false;
    }

    
    // Update is called once per frame
    private void Update()
    {
        if (playerTurn || enemiesMoving)
            return;

        StartCoroutine(MoveEnemies());
    }
}
