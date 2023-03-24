using UnityEngine;

public class CameraRender : MonoBehaviour
{
    private Camera cam;
    private Bounds camBounds;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Calculate the camera's view frustum bounds
        camBounds = new Bounds(cam.transform.position, new Vector3(cam.farClipPlane * 2, cam.farClipPlane * 2, cam.farClipPlane * 2));

        // Loop through all the game objects that have a renderer component
        foreach (Renderer renderer in FindObjectsOfType<Renderer>())
        {
            // Check if the renderer bounds are within the camera's view
            if (camBounds.Intersects(renderer.bounds))
            {
                // Enable the renderer if it's within the camera's view
                renderer.enabled = true;
            }
            else
            {
                // Disable the renderer if it's outside the camera's view
                renderer.enabled = false;
            }
        }
    }
}