using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    public float snakeSpeed = 1.0f;

    public BodyPart bodyPrefab = null;

    public Sprite tailSprite = null;
    public Sprite bodySprite = null;

    public SnakeHead snakeHead = null ;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        snakeHead.ResetSnake();
    }
}
