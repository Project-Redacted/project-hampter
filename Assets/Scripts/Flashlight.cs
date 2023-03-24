using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light flashlight; // Reference to the flashlight Light component
    public KeyCode toggleKey = KeyCode.E; // Key to toggle the flashlight on/off
    private bool isOn = false; // Flag to keep track of whether the flashlight is on or off
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        flashlight = GetComponentInChildren<Light>();
        flashlight.enabled = false;
    }
    void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            // Toggle the flashlight on/off
            isOn = !isOn;
            flashlight.enabled = isOn;
            audioSource.Play();

          //audioClip = GetComponent<AudioClip>();

        }
    }
}