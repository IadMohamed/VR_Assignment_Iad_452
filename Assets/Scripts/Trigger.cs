using System; // Required for basic system functionalities.
using UnityEngine; // Required for all Unity-specific functionalities (e.g., MonoBehaviour, GameObject, etc.).
using UnityEngine.Events; // Required for using UnityEvent, which allows custom events to be set up in the inspector.

// The Trigger class inherits from MonoBehaviour, making it a component that can be attached to a GameObject in Unity.
public class Trigger : MonoBehaviour
{
    // [SerializeField] makes a private field visible and editable in the Unity Inspector.
    // This string will be used to filter which GameObjects can interact with this trigger based on their tag.
    [SerializeField]
    private string tagFilter;

    // This UnityEvent will be invoked when another collider enters this trigger.
    // Events allow designers to hook up custom actions directly in the Inspector without writing more code.
    [SerializeField]
    private UnityEvent onTriggerEnter;

    // This UnityEvent will be invoked when another collider exits this trigger.
    [SerializeField]
    private UnityEvent onTriggerExit;

    /// <summary>
    /// Called when another collider enters a trigger attached to this object.
    /// This function is part of Unity's physics system and requires 'isTrigger' to be true on a Collider component.
    /// </summary>
    /// <param name="other">The Collider that entered this trigger.</param>
    void OnTriggerEnter(Collider other)
    {
        // Check if the tagFilter is not empty AND if the other GameObject's tag does NOT match the tagFilter.
        // If both conditions are true, it means we should ignore this collision.
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        // If the tag matches (or no tagFilter is set), invoke the 'onTriggerEnter' event.
        // Any functions hooked up to this event in the Inspector will now be called.
        onTriggerEnter.Invoke();
    }

    /// <summary>
    /// Called when another collider exits a trigger attached to this object.
    /// This function is part of Unity's physics system and requires 'isTrigger' to be true on a Collider component.
    /// </summary>
    /// <param name="other">The Collider that exited this trigger.</param>
    void OnTriggerExit(Collider other)
    {
        // Check if the tagFilter is not empty AND if the other GameObject's tag does NOT match the tagFilter.
        // If both conditions are true, it means we should ignore this collision.
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        // If the tag matches (or no tagFilter is set), invoke the 'onTriggerExit' event.
        // Any functions hooked up to this event in the Inspector will now be called.
        onTriggerExit.Invoke();
    }
}
