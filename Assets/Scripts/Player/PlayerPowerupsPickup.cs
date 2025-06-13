using UnityEngine;

public class PlayerPowerupsPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PowerupBase>(out var powerup))
        {
            powerup.PickUp(gameObject);
        }
    }
}
