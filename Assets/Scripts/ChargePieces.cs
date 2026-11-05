using Unity.VisualScripting;
using UnityEngine;

public class ChargePieces : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Boule boule))
        {
            boule.IncrementerNombrePieces();
            Destroy(gameObject);
        }
    }
}
