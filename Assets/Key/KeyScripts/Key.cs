using System;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] private KeyType keyType;
    public int KeyNumber;
    public enum KeyType
    {
        Red,
        Green,
        Blue
    }
         
    public KeyType GetKeyType()
    {
        return keyType;
            
    }

}
