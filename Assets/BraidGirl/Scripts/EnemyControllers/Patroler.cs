using System.Collections;
using System.Collections.Generic;
using BraidGirl;
using UnityEngine;

public class Patroler : MonoBehaviour
{
    [SerializeField]
    private float patrolSpeed;
    [SerializeField]
    private float angrySpeed;
    [SerializeField]
    private float goBackSpeed;
    [SerializeField]
    private int distanceOfPatrol;
    [SerializeField]
    private float distanceOfGoBack;
    [SerializeField]
    private float distanceOfAngry;
    [SerializeField]
    private Transform enemySpawnPoint;
    

    private Transform player1;
    private bool movingRight;

    bool patrol = false;
    bool angry = false;
    bool goBack = false;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private new void Update()
    {
        if (Vector3.Distance(transform.position, enemySpawnPoint.position) < distanceOfPatrol && angry == false)
        {
            patrol = true;
            angry = false;
            goBack = false;
        }
        if (Vector3.Distance(transform.position, player1.position) < distanceOfAngry)
        {
            angry = true;
            patrol = false;
            goBack = false;
        }
        if (Vector3.Distance(transform.position, player1.position) > distanceOfGoBack)
        {
            goBack = true;
            angry = false;
            patrol = false;
        }

        if (patrol == true)
        {
            Patrol();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }
    }

    private void Patrol()
    {
        if (transform.position.x > enemySpawnPoint.position.x + distanceOfPatrol)
        {
            movingRight = false;
            // Переделать;
            
        }
        else if (transform.position.x < enemySpawnPoint.position.x + distanceOfPatrol)
        {
            movingRight = true;
            
        }


        if (movingRight)
        {
            transform.position = new Vector3(transform.position.x + patrolSpeed * Time.deltaTime, 0f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - patrolSpeed * Time.deltaTime, 0f);
        }
    }

    private void Angry()
    {
        transform.position = Vector3.MoveTowards(transform.position, player1.position, angrySpeed * Time.deltaTime);
    }

    private void GoBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemySpawnPoint.position, goBackSpeed * Time.deltaTime);
    }
}
