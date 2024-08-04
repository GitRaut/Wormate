using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent (typeof(GrowManager))]
[RequireComponent(typeof(PlayerMovement))]

public class TriggerManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        if (collision.TryGetComponent(out InteractableObject obj))
            obj.Interact(gameObject);
    }
}
