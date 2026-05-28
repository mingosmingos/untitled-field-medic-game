using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private FixedJoint2D grabJoint;
    private Rigidbody2D grabbedBody;
    private GameObject nearbyObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleGrabInput();
        }
        // Debug.Log(nearbyObject);
    }

    void HandleGrabInput()
    {
        if (grabJoint != null)
        {
            Debug.Log("grabJoint is not null");
            Drop();
        }
        else if (nearbyObject != null)
        {
            Debug.Log("nearbyObject is not null");
            // Debug.Log(nearbyObject.name);
            Grab(nearbyObject);
        }
        // Debug.Log("Both null!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        nearbyObject = other.gameObject;
        Debug.Log("Entered");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == nearbyObject)
        {
            nearbyObject = null;
            // Debug.Log("Exited");
        }
    }

    void Grab(GameObject obj)
    {
        IGrabbable grabbable = obj.GetComponent<IGrabbable>();
        if (grabbable == null || !grabbable.IsGrabbable) return;

        grabbedBody = obj.GetComponent<Rigidbody2D>();
        if (grabbedBody == null) return;

        grabbable.OnGrabbed(); // NPC switches itself to Dynamic

        grabJoint = gameObject.AddComponent<FixedJoint2D>();
        grabJoint.connectedBody = grabbedBody;
    }

    void Drop()
    {
        Destroy(grabJoint);
        grabJoint = null;

        if (grabbedBody != null)
        {
            grabbedBody.GetComponent<IGrabbable>()?.OnDropped();
            grabbedBody = null;
        }
        // Debug.Log("Drop");
    }
}
