using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPlacement : MonoBehaviour

{
    public GameObject BloodPlane;


    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Spawnblood();
        }
    }

    void Spawnblood()
    {
        Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 5f, ground);
        Instantiate(BloodPlane, hit.point + new Vector3(0, 0.01f, 0), Quaternion.identity);
    }


}
