using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    Vector2 deltaPosition;

    public BodyPart following = null;

    private bool isTail = false;

    private SpriteRenderer spriteRenderer = null;

    const int PARTSREMEMBERED = 10;

    public Vector3[] previosPositions = new Vector3[PARTSREMEMBERED];

    public int setIndex = 0;
    public int getIndex = -(PARTSREMEMBERED - 1);

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    virtual public void Update()
    {
        Vector3 followPosition;
        if (following!=null)
        {
            if (following.getIndex>-1)
            {
                followPosition = following.previosPositions[following.getIndex];
            }
            else
            {
                followPosition = following.transform.position;
            }
        }
        else
        {
            followPosition = gameObject.transform.position;
        }
        previosPositions[setIndex].x = gameObject.transform.position.x;
        previosPositions[setIndex].y = gameObject.transform.position.y;
        previosPositions[setIndex].z = gameObject.transform.position.z;

        setIndex++;
        getIndex++;
        if (setIndex >= PARTSREMEMBERED)
            setIndex = 0;
        if (getIndex >= PARTSREMEMBERED)
            getIndex = 0;

        Vector3 newPosition;
        if (following != null) // i.e not the head
        {
            if (following.getIndex > -1)
            {
                newPosition = followPosition;
            }
            else
            {
                newPosition = following.transform.position;
            }

            newPosition.z += 0.01f;

            SetMovement(newPosition - gameObject.transform.position);
            UpdateDirection();
            UpdatePosition();
        }
    }

    public void SetMovement(Vector2 movement)
    {
        deltaPosition = movement;
    }

    public void UpdatePosition()
    {
        gameObject.transform.position += (Vector3)deltaPosition;
    }

    public void UpdateDirection()
    {
        if (deltaPosition.y > 0) //up
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        else if (deltaPosition.y < 0) //down
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 180);
        else if (deltaPosition.x < 0) //left 
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
        else if (deltaPosition.x > 0) //right
            gameObject.transform.localEulerAngles = new Vector3(0, 0, -90);
    }

    public void TurnIntoTail()
    {
        isTail = true;
        spriteRenderer.sprite = GameController.instance.tailSprite;
    }
    public void TurnIntoBodyPart()
    {
        isTail = false;
        spriteRenderer.sprite = GameController.instance.bodySprite;
    }



}
