using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] patrolPoints;
    float speed = 5f;
    int pointsSize, currentPointIndex;
    bool isMovingForward = true, canMove = true, calledChangeIndex;

    void Start()
    {
        pointsSize = patrolPoints.Length;
        transform.position = patrolPoints[currentPointIndex].position;
        if (pointsSize > 1) currentPointIndex++;
    }

    void VerifyFlip()
    {
        float actualX = transform.position.x;
        float patrolX = patrolPoints[currentPointIndex].position.x;

        if (patrolX > actualX) GetComponent<SpriteRenderer>().flipX = true;
        else if (patrolX < actualX) GetComponent<SpriteRenderer>().flipX = false;
    }

    void VerifyIndex()
    {
        if (transform.position == patrolPoints[currentPointIndex].position && !calledChangeIndex)
        {
            calledChangeIndex = true;
            StartCoroutine(ChangePointIndex());
        }
    }

    IEnumerator ChangePointIndex()
    {
        yield return new WaitForSeconds(0.5f); 
        ChangeIndex();
    }

    void Update()
    {
        VerifyFlip();
        VerifyIndex();
    }

    void CalculateAndMove()
    {
        if (canMove)
        {
            Vector2 moveAmount = Vector2.MoveTowards(GetComponent<Rigidbody2D>().position, patrolPoints[currentPointIndex].position, speed * Time.fixedDeltaTime);
            GetComponent<Rigidbody2D>().MovePosition(moveAmount);  
        }
    }

    void FixedUpdate() => CalculateAndMove();

    void ChangeIndex()
    {
        if (isMovingForward && currentPointIndex + 1 < pointsSize) currentPointIndex++;
        
        else if (isMovingForward && currentPointIndex + 1 >= pointsSize)
        {
            currentPointIndex--;
            isMovingForward = false;
        }
        
        else if (!isMovingForward && currentPointIndex - 1 >= 0) currentPointIndex--;
        
        else if (!isMovingForward && currentPointIndex - 1 < 0)
        {
            currentPointIndex++;
            isMovingForward = true;
        }
        
        calledChangeIndex = false;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Player")) trigger.GetComponent<PlayerController>().ProcessHit();
    }

    public void GetHit()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
        GetComponent<Animator>().SetTrigger("Blink");
    }
}