//using UnityEngine;

//public class Move : MonoBehaviour
//{
    //public float rotationSpeed = 50f;

    // Update is called once per frame
   // void Update()
   // {
    //    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + (rotationSpeed * Time.deltaTime), 0);
   // }

//}
using UnityEngine;

public class Move : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float movementSpeed = 1f;
    public float movementRange = 1f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Rotate the object around the Y axis at the specified speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Move the object up and down within a specified range
        Vector3 newPosition = startPosition + new Vector3(0, Mathf.Sin(Time.time * movementSpeed) * movementRange, 0);
        transform.position = newPosition;
    }
}