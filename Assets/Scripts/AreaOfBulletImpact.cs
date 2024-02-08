using UnityEngine;

public class AreaOfBulletImpact : MonoBehaviour
{

    void FixedUpdate()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Wall Piece") && other.transform.TryGetComponent<WallPieceScript>(out WallPieceScript wallPieceScript))
        {
            wallPieceScript.TakeDamage(transform.position);
            Destroy(gameObject);
        }
    }

}
