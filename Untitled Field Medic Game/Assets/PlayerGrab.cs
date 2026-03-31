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
            if (grabJoint == null && nearbyObject != null)
            {
                var grab = nearbyObject.GetComponent<Friendly>();
                Debug.Log(grab.isGrabbable);

                if (grab != null && grab.isGrabbable)
                {
                    Grab(nearbyObject);
                }
            }
            else
            {
                Drop();
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

        grabJoint = gameObject.AddComponent<FixedJoint2D>();
        grabJoint.connectedBody = grabbedBody;
    }

    void Drop()
    {
        if (grabJoint != null)
        {
            Destroy(grabJoint);
            grabJoint = null;
            grabbedBody = null;
        }
    }
}
