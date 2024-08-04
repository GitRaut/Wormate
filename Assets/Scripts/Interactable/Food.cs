using Unity.Netcode;
using UnityEngine;

public class Food : InteractableObject
{
    [SerializeField] private float growFactor;
    [SerializeField] public GameObject prefab;

    protected override void OnInteract(GameObject obj)
    {
        obj.GetComponent<GrowManager>().Eat(growFactor);
    }

    protected override void OnDeInteract(GameObject obj)
    {
        NetworkObjectPool.Singleton.ReturnNetworkObject(NetworkObject, prefab);

        if (NetworkObject != null && NetworkObject.IsSpawned)
            NetworkObject.Despawn(true);
    }
}
