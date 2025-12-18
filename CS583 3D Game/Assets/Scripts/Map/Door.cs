using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorBody;
    public Transform openPos;
    public Transform closePos;

    public float speed;
    public float stoppingDistance;

    bool doorOpen;
    int occupants = 0;

    public SpriteRenderer[] doorLocks;
    public Sprite lockOpen;
    public Sprite lockClose;
    
    void Update()
    {
        if(doorOpen)
        {
            if (Vector3.Distance(doorBody.position, openPos.position) > stoppingDistance)
            {
                doorBody.position = Vector3.MoveTowards(doorBody.position, openPos.position, speed * Time.deltaTime);
            }
        }
        else
        {
            if (Vector3.Distance(doorBody.position, closePos.position) > stoppingDistance)
            {
                doorBody.position = Vector3.MoveTowards(doorBody.position, closePos.position, speed * Time.deltaTime);
            }
        }

        foreach (SpriteRenderer sr in doorLocks)
        {
            if(doorOpen)
            {
                sr.sprite = lockOpen;
            }
            else
            {
                sr.sprite = lockClose;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("DoorOpen");
            occupants++;
            doorOpen = true;
        } 
        else if (other.CompareTag("Enemy"))
        {
            //FindObjectOfType<AudioManager>().Play("DoorOpen");
            other.GetComponent<EnemyStats>().SetDoor(this);
            occupants++;
            doorOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("DoorOpen");
            CheckToClose();
        }
        else if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            CheckToClose();
        }
        else if (other.CompareTag("Enemy"))
        {
            //FindObjectOfType<AudioManager>().Play("DoorOpen");
            other.GetComponent<EnemyStats>().SetDoor(null);
            CheckToClose();
        }
    }

    public void CheckToClose()
    {
        occupants--;
        if (occupants <= 0)
            doorOpen = false;
    }
}
