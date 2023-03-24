using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMove : MonoBehaviour
{
    public List<GameObject> Locations;
    public int Speed = 1;
    public GameObject CurrentLocation;
    
    // On start
    void Start()
    {
        // Set the current location to the first location in the list
        CurrentLocation = Locations[0];
    }
    
    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.Lerp(transform.position, CurrentLocation.transform.position, Time.deltaTime * Speed);
       transform.rotation = Quaternion.Lerp(transform.rotation, CurrentLocation.transform.rotation, Time.deltaTime * Speed);
    }

    public void MoveToLocation(int location)
    {
        // Set the current location to the location in the list
        CurrentLocation = Locations[location];
    }
}
