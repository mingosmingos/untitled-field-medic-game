using UnityEngine;

public class VisionBehaviour : MonoBehaviour
{
    private int i = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        NPCBehaviour unit = GetComponentInParent<NPCBehaviour>();
        i++;
        Debug.Log(i);
        if (unit != null)
        {
            unit.OnVisionDetected(other);
        }

        // Debug.Log("Vision detected: " + other.gameObject.name);
    }
}
