using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FoodManager : NetworkBehaviour
{
    public static FoodManager instance;

    [SerializeField] private GameObject foodPref;
    [SerializeField] private int maxCount;
    [SerializeField] private float generateRadius;

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += SpawnFoodStart;
    }

    private void SpawnFoodStart()
    {
        Debug.Log("SpawnFoodStart");
        NetworkManager.Singleton.OnServerStarted -= SpawnFoodStart;
        NetworkObjectPool.Singleton.InitializePool();

        for (int i = 0; i < 10; i++) SpawnFood();
    }

    private void SpawnFood()
    {
        Quaternion rot = Random.rotation;
        rot = new Quaternion(0, 0, rot.z, rot.w);
        Vector3 pos = Random.insideUnitCircle * generateRadius;

        NetworkObject obj = NetworkObjectPool.Singleton.GetNetworkObject(foodPref, pos, rot);

        Food foodComponent = obj.GetComponent<Food>();
        if (foodComponent != null) foodComponent.prefab = foodPref;

        if (!obj.IsSpawned) obj.Spawn(true);

        StartCoroutine(GenerateFood());
    }

    private IEnumerator GenerateFood()
    {
        while (NetworkManager.Singleton.ConnectedClients.Count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            if (NetworkObjectPool.Singleton.GetCurrentPrefabCount(foodPref) < maxCount) SpawnFood();
        }
    }
}
