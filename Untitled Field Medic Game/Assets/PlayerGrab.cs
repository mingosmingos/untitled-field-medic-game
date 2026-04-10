using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private FixedJoint2D grabJoint;
    private Rigidbody2D grabbedBody;
    private GameObject nearbyObject;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleGrabInput();
        }
    }

    void HandleGrabInput()
    {
        if (grabJoint != null)
        {
            Drop();
        }
        else
        {
            if (nearbyObject != null)
            {
                Grab(nearbyObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        nearbyObject = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == nearbyObject)
        {
            nearbyObject = null;
        }
    }

    void Grab(GameObject obj)
    {
        grabbedBody = obj.GetComponent<Rigidbody2D>();
        if (grabbedBody == null) return;

        IGrabbable grabbable = obj.GetComponent<IGrabbable>();
        if (grabbable == null || !grabbable.IsGrabbable) return;

        grabJoint = gameObject.AddComponent<FixedJoint2D>();
        grabJoint.connectedBody = grabbedBody;
        Debug.Log("Grab");
    }

    void Drop()
    {
        Destroy(grabJoint);
        grabJoint = null;
        grabbedBody = null;
        Debug.Log("Drop");
    }
}
