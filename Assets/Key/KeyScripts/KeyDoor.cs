using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{

    [SerializeField] private Key.KeyType keyType;

    [SerializeField] int keyNeeded;
    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Key key = other.GetComponent<Key>();
        if (key != null) 
        {
            if(key.KeyNumber == keyNeeded)
            {
                other.gameObject.SetActive(false);
                alertEnemies();
                Invoke("OpenDoor", 10);

            }
        }
    }
    public void alertEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        foreach(GameObject enemy in enemies)
        {
            if(closest == null)
            {
                closest = enemy;
            }
            if(Vector3.Distance(closest.transform.position, transform.position) > Vector3.Distance(enemy.transform.position, transform.position))
            {
                closest = enemy;
            }

        }
        closest.GetComponent<AIMovement>().Alert(transform.position);
            

        
    }
}
