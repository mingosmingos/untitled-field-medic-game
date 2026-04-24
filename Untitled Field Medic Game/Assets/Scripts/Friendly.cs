using UnityEngine;

public class Friendly : MonoBehaviour, IDamageable, IGrabbable
{
    public bool IsGrabbable { get; private set; } = false;
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
            IsGrabbable = true;
            Debug.Log("I'm dead");
        }
        else
        {
            Debug.Log("I'm hit");
        }
    }
}
