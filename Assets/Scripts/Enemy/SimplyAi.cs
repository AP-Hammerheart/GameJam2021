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
    public int maxRange;
    public Vector3 startPosition;
    public float speed;

    void Start()
    {
        startPosition = transform.position;

        int x = UnityEngine.Random.Range(0,1);
        if (x == 1)
            currentDir = Directions.Right;
        else
            currentDir = Directions.Left;
    }

    // Update is called once per frame
    void Update()
    {
        TimeToTurnAround();
        Patrol();
    }

    void Patrol()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (currentDir == Directions.Right)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            
        }

        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void TimeToTurnAround()
    {
        if (transform.position.x >= startPosition.x + maxRange)
        {
            if (currentDir == Directions.Right)
                currentDir = Directions.Left;
            else
                currentDir = Directions.Right;
        }
    }



}
