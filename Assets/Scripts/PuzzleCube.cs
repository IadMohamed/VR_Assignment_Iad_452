using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleCube : MonoBehaviour
{
    public CubeColor color;
    public Transform originalPosition;

    private XRGrabInteractable grab;
    private bool placed = false;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        originalPosition = new GameObject($"{name}_OriginalPos").transform;
        originalPosition.position = transform.position;
        originalPosition.rotation = transform.rotation;
    }

    public void ReturnToOriginal()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // disable physics before moving
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = originalPosition.position;
        transform.rotation = originalPosition.rotation;

        rb.isKinematic = false; // re-enable physics
    }

    public void ResetCube()
    {
        placed = false;
        grab.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        ReturnToOriginal();
    }
    public void LockPlacement()
    {
        placed = true;
        grab.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Snap the cube to height 0.6
        transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
    }

    public bool IsPlaced() => placed;


}

