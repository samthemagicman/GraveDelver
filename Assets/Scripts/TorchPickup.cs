using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPickup : MonoBehaviour, IPickup
{
    public void Activate()
    {
        TorchManager torchManager = PlayerController.singleton.gameObject.GetComponent<TorchManager>();

        torchManager.AddToRadius(5);
    }
}
