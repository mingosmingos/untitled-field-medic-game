using UnityEngine;

public class Friendly : MonoBehaviour
{
    public bool isGrabbable = false;
    public int healthPoints = 1;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage()
    {
        healthPoints--;
        if (healthPoints < 1)
        {
            isGrabbable = true;
        }
    }
}
