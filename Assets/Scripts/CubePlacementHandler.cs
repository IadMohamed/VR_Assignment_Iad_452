using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CubePlacementHandler : MonoBehaviour
{
    private Vector3 originalPosition;
    private XRGrabInteractable grabInteractable;
    public AudioClip buzzSound; // Drag your buzz sound file here in Unity
    private AudioSource audioSource;
    private bool isLocked = false; // To prevent further movement after correct placement

    void Start()
    {
        originalPosition = transform.position; // Store where the cube started
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Listen for when the cube is released (either correctly or incorrectly)
        grabInteractable.selectExited.AddListener(OnSelectExited);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // Add an AudioSource if the cube doesn't have one
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // This checks if the cube was released but NOT placed into a valid socket
        // (meaning no interactor is currently selecting it, and it's not locked).
        if (grabInteractable.selectingInteractor == null && !isLocked)
        {
            // If it wasn't placed correctly (e.g., dropped on the floor or wrong socket)
            transform.position = originalPosition; // Move it back to its start
            if (buzzSound != null)
            {
                audioSource.PlayOneShot(buzzSound); // Play the buzz sound
            }
        }
    }

    // This method will be called by the PuzzleManager when the cube is correctly placed.
    public void LockCube()
    {
        isLocked = true;
        grabInteractable.enabled = false; // Stop the cube from being grabbed again

        // Make the cube stop reacting to physics if it's not already handled by the socket
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Makes the Rigidbody not affected by physics forces
        }
    }

    // This method will be called by the PuzzleManager when restarting the puzzle
    public void ResetCube()
    {
        isLocked = false;
        grabInteractable.enabled = true; // Allow grabbing again
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Allow physics forces again
        }
        transform.position = originalPosition; // Return to original position
    }
}