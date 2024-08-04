using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FoodManager : NetworkBehaviour
{
    public static FoodManager instance;

    [SerializeField] private List<GameObject> foodPrefabs = new List<GameObject>();
    [SerializeField] private int maxCount;
    [SerializeField] private float generateRadius;

    private List<GameObject> foodList = new List<GameObject>();
    private GameObject curPref;

    private void Awake()
    {
        FoodManager[] instances = FindObjectsOfType<FoodManager>();
        int count = instances.Length;
        if (instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        else if (count > 0)
        {
            if (count == 1)
                instance = instances[0];
            for (int i = 1; i < instances.Length; i++)
                Destroy(instances[i]);
            instance = instances[0];
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += SpawnFoodStart;
        StartCoroutine(GenerateFood());
    }

    private void SpawnFoodStart()
    {
        Debug.Log("SpawnFoodStart");
        NetworkManager.Singleton.OnServerStarted -= SpawnFoodStart;
        NetworkObjectPool.Singleton.InitializePool();

        for (int i = 0; i < 10; i++)
            SpawnFood();
    }

    private void SpawnFood()
    {
        Quaternion rot = Random.rotation;
        rot = new Quaternion(0, 0, rot.z, rot.w);
        Vector3 pos = Random.insideUnitCircle * generateRadius;
        int rand = Random.Range(0, foodPrefabs.Count);
        curPref = foodPrefabs[rand];

        NetworkObject obj = NetworkObjectPool.Singleton.GetNetworkObject(curPref, pos, rot);

        Food foodComponent = obj.GetComponent<Food>();
        if (foodComponent != null)
            foodComponent.prefab = foodPrefabs[rand];

        if (!obj.IsSpawned)
            obj.Spawn(true);

        StartCoroutine(GenerateFood());
    }

    private IEnumerator GenerateFood()
    {
        while (NetworkManager.Singleton.ConnectedClients.Count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            if (NetworkObjectPool.Singleton.GetCurrentPrefabCount(curPref) < maxCount) SpawnFood();
        }
    }
}
