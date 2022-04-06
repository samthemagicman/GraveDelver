using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    TorchManager torchManager;

    private void Start()
    {
        torchManager = GetComponent<TorchManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickup pickup = null;
        collision.TryGetComponent<IPickup>(out pickup);

        if (pickup != null)
        {
            Destroy(collision.gameObject);
            pickup.Activate();
        }
    }
}
