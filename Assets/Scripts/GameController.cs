using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    const float WIDTH = 3.7f;
    const float HEIGHT = 7f;


    public float snakeSpeed = 1.0f;

    public BodyPart   bodyPrefab = null;
    public GameObject rockPrefab = null;
    public GameObject eggPrefab = null;
    public GameObject goldenEggPrefab = null;

    public Sprite tailSprite;
    public Sprite bodySprite;

    public SnakeHead snakeHead = null ;

    public bool alive = false;

    public bool waitingForPlayer = true;

    List<Egg> eggs = new List<Egg>();

    int noOFEggsTOLevelUp = 0;
    int level = 0;

    public int score = 0;
    public int hiScore = 0;

    public Text scoreText;
    public Text hiScoreText;

    public Text gameOverText;
    public Text tapToPlayText;


    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        CreateWalls();
        alive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForPlayer == true)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    StartGamePlay();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                StartGamePlay();
            }
        }
    }


    void LevelUp()
    {
        
        level++;
        noOFEggsTOLevelUp = 4 + (level * 2);
        snakeSpeed = 1f + (level / 4f);
        if (snakeSpeed > 6) snakeSpeed = 6;

        snakeHead.ResetSnake();

        CreateEgg();
    }
    public void GameOver()
    {   
        alive = false;
        waitingForPlayer = true;

        gameOverText.gameObject.SetActive(true);
        tapToPlayText.gameObject.SetActive(true);
    }


    private void StartGamePlay()
    {
        score = 0;
        level = 0;

        DestroyOldEggs();
        LevelUp();

        gameOverText.gameObject.SetActive(false);
        tapToPlayText.gameObject.SetActive(false);
        waitingForPlayer = false;
        alive = true;
        
    }

    public void EggEaten(Egg egg)
    {
        score++;
        scoreText.text = "Score = " + score;

        noOFEggsTOLevelUp--;
        if (noOFEggsTOLevelUp == 0)
        {
            score += 10;
            LevelUp();
        }

        else if (noOFEggsTOLevelUp == 1) // last egg
        {
            CreateEgg(true);
        }
        else
            CreateEgg(false);

        if (score > hiScore)
        {
            hiScore = score;
            hiScoreText.text = "High Score = " + hiScore;
        }

        eggs.Remove(egg);
        Destroy(egg.gameObject);
    }
    void CreateWalls()
    {
        float z = -1f;

        Vector3 start = new Vector3(-WIDTH, -HEIGHT, z);    // LEFT
        Vector3 finish = new Vector3(-WIDTH, +HEIGHT, z);
        CreateWall(start, finish);

        start = new Vector3(+WIDTH, -HEIGHT, z);            // RIGHT
        finish = new Vector3(+WIDTH, +HEIGHT, z);
        CreateWall(start, finish);

        start = new Vector3(-WIDTH, -HEIGHT, z);            //BOTTOM
        finish = new Vector3(+WIDTH, -HEIGHT, z);
        CreateWall(start, finish);

        start = new Vector3(-WIDTH, +HEIGHT, z);            //TOP
        finish = new Vector3(+WIDTH, +HEIGHT, z);
        CreateWall(start, finish);
    }

    void CreateWall(Vector3 start , Vector3 finish)
    {
        float distance = Vector3.Distance(start, finish);
        int noOfRocks = (int)(distance * 3f);
        Vector3 delta = (finish - start) / noOfRocks;

        Vector3 position = start;
        for (int rock = 0; rock < noOfRocks; rock++)
        {
            float rotation = Random.Range(0, 360);
            float scale = Random.Range(1.5f, 2f);
            CreateRock(position, rotation, scale);
            position += delta;
        }

    }

    void CreateRock(Vector3 position , float rotation , float scale)
    {
        GameObject rock = Instantiate(rockPrefab, position, Quaternion.Euler(0, 0, rotation));
        rock.transform.localScale = new Vector3(scale, scale, 1);

    }

    void CreateEgg(bool golden = false)
    {
        Vector3 position;
        position.x = -WIDTH + Random.Range(1f, (WIDTH * 2f) - 2f);
        position.y = -HEIGHT+ Random.Range(1f, (HEIGHT * 2f) - 2f);
        position.z = -1f;
        Egg egg = null;

        if (golden)
        {
           egg =  Instantiate(goldenEggPrefab, position, Quaternion.identity).GetComponent<Egg>();
        }
        else
        {
            egg = Instantiate(eggPrefab, position, Quaternion.identity).GetComponent<Egg>();
        }

        eggs.Add(egg);
       
    }

   private void DestroyOldEggs()
    {
        foreach (Egg egg in eggs)
        {
            Destroy(egg.gameObject);
        }
        eggs.Clear();
    }
}
