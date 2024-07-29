using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float speed = 5f;
    int pointsSize, currentPointIndex;
    bool isMovingForward = true, canMove = true, calledChangeIndex;

    void Start()
    {
        pointsSize = patrolPoints.Length;
        transform.position = patrolPoints[currentPointIndex].position;
        if (pointsSize > 1) currentPointIndex++;
    }

    void verifyFlip()
    {
        float actualX = transform.position.x;
        float patrolX = patrolPoints[currentPointIndex].position.x;

        if (patrolX > actualX) GetComponent<SpriteRenderer>().flipX = true;
        else if (patrolX < actualX) GetComponent<SpriteRenderer>().flipX = false;
    }

    void verifyIndex()
    {
        if (transform.position == patrolPoints[currentPointIndex].position && !calledChangeIndex)
        {
            calledChangeIndex = true;
            StartCoroutine(changePointIndex());
        }
    }

    IEnumerator changePointIndex()
    {
        yield return new WaitForSeconds(0.5f); 
        changeIndex();
    }

    void Update()
    {
        verifyFlip();
        verifyIndex();
    }

    void calculateAndMove()
    {
        if (canMove)
        {
            Vector2 moveAmount = Vector2.MoveTowards(GetComponent<Rigidbody2D>().position, patrolPoints[currentPointIndex].position, speed * Time.fixedDeltaTime);
            GetComponent<Rigidbody2D>().MovePosition(moveAmount);  
        }
    }

    void FixedUpdate() => calculateAndMove();

    void changeIndex()
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) other.GetComponent<PlayerHealth>().ProcessHit();
    }

    public void GetHit()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
        GetComponent<Animator>().SetBool("Blink", true);
        GetComponent<Animator>().SetBool("Die", true);
    }
}