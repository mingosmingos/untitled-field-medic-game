using UnityEngine;
using TMPro;
using System.Collections; 

public class SafeZone : MonoBehaviour
{
    public GameObject savedMessage;
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
        IGrabbable grabbable = other.GetComponent<IGrabbable>();
        if (grabbable != null && grabbable.IsGrabbable)
        {
            StartCoroutine(ShowMessage());
        }
    }

    IEnumerator ShowMessage()
    {
        savedMessage.SetActive(true);
        yield return new WaitForSeconds(2f);
        savedMessage.SetActive(false);
    }
}
