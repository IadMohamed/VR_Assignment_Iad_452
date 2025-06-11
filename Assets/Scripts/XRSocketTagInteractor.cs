using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events; // Needed for UnityEvent

public class XRSocketTagInteractor : XRSocketInteractor
{
    [Tooltip("Only objects with this tag can be snapped into this socket.")]
    public string targetTag; // This is where you'll type the matching tag in Unity

    // This event will be triggered when an object is correctly placed in this socket.
    // We use [field: SerializeField] to make it visible and assignable in the Inspector.
    [field: SerializeField] public UnityEvent OnItemPlaced { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (OnItemPlaced == null)
        {
            OnItemPlaced = new UnityEvent();
        }
    }

    protected override void Start()
    {
        base.Start(); // Call the original Start method from XRSocketInteractor
        selectEntered.AddListener(OnSocketSelectEntered); // Listen for when an object is successfully placed
    }

    private void OnSocketSelectEntered(SelectEnterEventArgs args)
    {
        // When an item is placed, we trigger our custom event
        OnItemPlaced.Invoke();
    }

    public override bool CanHover(XRBaseInteractable interactable)
    {
        // Only allow the transparent preview (hover) if the object has the correct tag.
        return base.CanHover(interactable) &&
               (interactable.transform.CompareTag(targetTag));
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        // Only allow the object to snap (select) if it has the correct tag.
        return base.CanSelect(interactable) &&
               (interactable.transform.CompareTag(targetTag));
    }
}