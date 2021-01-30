using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplyAi : MonoBehaviour
{

    public enum Directions
    {
        Right, Left
    }

    public Directions currentDir;
    public float maxRange;
    public Vector3 rightPosition, leftPosition;
    public Vector3 rightPositionOffset, leftPositionOffset;
    public float speed;

    void Start()
    {
        rightPosition = new Vector3(transform.position.x + maxRange, transform.position.y, 0);
        leftPosition = new Vector3(transform.position.x - maxRange, transform.position.y, 0);

        rightPositionOffset = new Vector3((transform.position.x + maxRange * 2), transform.position.y, 0);
        leftPositionOffset = new Vector3((transform.position.x - (maxRange * 2)), transform.position.y, 0);

        //int x = UnityEngine.Random.Range(0, 1);
        //if (x == 1)
        //    currentDir = Directions.Right;
        //else
        //    currentDir = Directions.Left;
    }

    // Update is called once per frame
    void Update()
    {
        TimeToTurnAround();
        Patrol();
    }

    void Patrol()
    {


        if (currentDir == Directions.Right)
        {
            //transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(rightPositionOffset * speed * Time.deltaTime);
        }

        else
        {
            //transform.eulerAngles = new Vector3(0, -180, 0);
            transform.Translate(leftPositionOffset * speed * Time.deltaTime);
        }
    }

    void TimeToTurnAround()
    {
        if (transform.position.x >= rightPosition.x && currentDir == Directions.Right)
        {
            currentDir = Directions.Left;
        }

        if (transform.position.x <= leftPosition.x && currentDir == Directions.Left)

        { currentDir = Directions.Right; }
    }



}
