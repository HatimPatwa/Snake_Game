using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        CreateWalls();
        StartGame();
        CreatEgg();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        snakeHead.ResetSnake();
    }

    void CreateWalls()
    {

        Vector3 start = new Vector3(-WIDTH, -HEIGHT, 0);    // LEFT
        Vector3 finish = new Vector3(-WIDTH, +HEIGHT, 0);
        CreateWall(start, finish);

        start = new Vector3(+WIDTH, -HEIGHT, 0);            // RIGHT
        finish = new Vector3(+WIDTH, +HEIGHT, 0);
        CreateWall(start, finish);

        start = new Vector3(-WIDTH, -HEIGHT, 0);            //BOTTOM
        finish = new Vector3(+WIDTH, -HEIGHT, 0);
        CreateWall(start, finish);

        start = new Vector3(-WIDTH, +HEIGHT, 0);            //TOP
        finish = new Vector3(+WIDTH, +HEIGHT, 0);
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

    void CreatEgg(bool golden = false)
    {
        Vector3 position;
        position.x = -WIDTH + Random.Range(1f, (WIDTH * 2f) - 2f);
        position.y = -HEIGHT+ Random.Range(1f, (HEIGHT * 2f) - 2f);
        position.z = 0;

        if (golden)
        {
            Instantiate(goldenEggPrefab, position, Quaternion.identity);
        }
        else
        {
            Instantiate(eggPrefab, position, Quaternion.identity);
        }
       
    }
}
