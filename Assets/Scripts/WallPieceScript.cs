using UnityEngine;

public class WallPieceScript : MonoBehaviour
{
    [SerializeField] Material[] materials;
    private Rigidbody rb;
    private MeshRenderer mr;
    private int amountOfHits = 0;
    private bool isDestroyed = false;
    private bool toDisappear = false;
    private float t = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    public void TakeDamage(Vector3 pointOfImpact)
    {
        if (amountOfHits == 2 && isDestroyed == false)
        {
            transform.localScale = transform.localScale * 0.7f;
            rb.isKinematic = false;
            rb.AddExplosionForce(2000f, pointOfImpact, 100);
            isDestroyed = true;
            Invoke("ToDisappear", 2);
        }
        else if(amountOfHits < 2 && isDestroyed == false)
        {
            amountOfHits++;
            mr.material = materials[amountOfHits];
        }
    }

    public void TakeDamageFromSuitcase(Vector3 pointOfImpact)
    {

        mr.material = materials[2];

        transform.localScale = transform.localScale * 0.7f;
        rb.isKinematic = false;
        rb.AddExplosionForce(400, pointOfImpact, 30);
        isDestroyed = true;
        Invoke("ToDisappear", 2);

    }

    private void ToDisappear()
    {
        toDisappear = true;
    }

    private void Update()
    {
        if (toDisappear)
        {
            t += Time.deltaTime / 2;

            Vector3 newScale = Vector3.Lerp(transform.localScale, new Vector3(1,0.1f,1), t);
            transform.localScale = newScale;

            if (transform.localScale.x <= 1.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
