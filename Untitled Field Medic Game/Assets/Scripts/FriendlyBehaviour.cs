using UnityEngine;
using TMPro;

public class FriendlyBehaviour : MonoBehaviour, IGrabbable, IDamageable
{
    public bool IsGrabbable { get; private set; } = true;
    public int healthPoints = 3;

    [SerializeField] private TextMeshProUGUI healthPointsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealthPointsText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        healthPoints--;
        UpdateHealthPointsText();
        if (healthPoints < 1)
        {
            GetComponent<WaypointNavigation>().enabled = false;
            IsGrabbable = true;
        }
    }

    private void UpdateHealthPointsText()
    {
        if (healthPointsText == null) return;

        healthPointsText.text = healthPoints.ToString();
    }
}