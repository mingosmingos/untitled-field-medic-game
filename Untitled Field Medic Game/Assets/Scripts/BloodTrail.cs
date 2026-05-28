using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BloodTrail : MonoBehaviour
{
    public RenderTexture bloodRT;       // Assign the RT here
    public Material stampMaterial;      // Assign BloodStamp material
    public float bleedInterval = 0.05f; // Stamps per second (20/sec)
    public float splatSize = 0.02f;     // World-to-UV scale
    public bool isBleeding = false;

    private Renderer groundRenderer;
    private float timer;
    private Vector3 lastPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundRenderer = FindObjectOfType<Renderer>(); // Or assign explicitly
        splatSize /= 10f; // Adjust based on your ground scale
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBleeding) return;

        timer += Time.deltaTime;
        if (timer >= bleedInterval)
        {
            timer = 0f;
            StampBlood(transform.position);
            lastPos = transform.position;
        }
    }

    void StampBlood(Vector3 worldPos)
    {
        // Convert world pos to UV (0-1)
        Vector2 uv = new Vector2(
            Mathf.InverseLerp(-5f, 5f, worldPos.x), // Adjust bounds to match ground
            Mathf.InverseLerp(-5f, 5f, worldPos.z)
        );
        uv.y = 1f - uv.y; // Flip Y if needed

        stampMaterial.SetVector("_StampPos", new Vector4(uv.x, uv.y, 0, 0));
        stampMaterial.SetFloat("_StampScale", splatSize);

        // Paint onto RT
        Graphics.Blit(null, bloodRT, stampMaterial);
    }

    public void SetBleeding(bool bleed) => isBleeding = bleed;
}
