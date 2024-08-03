using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance;

    [SerializeField] private List<GameObject> foodPrefabs = new List<GameObject>();
    [SerializeField] private int maxCount;
    [SerializeField] private float generateRadius;

    private List<GameObject> foodList = new List<GameObject>();

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

    private void Start() => StartCoroutine(GenerateFood());

    public void AddFood(GameObject food)
    {
        foodList.Add(food);
    }

    public void RemoveFood(GameObject food)
    {
        foodList.Remove(food);
    }

    private IEnumerator GenerateFood()
    {
        yield return new WaitForSeconds(0.1f);

        if (foodList.Count < maxCount)
        {
            Quaternion rot = Random.rotation;
            rot = new Quaternion(0, 0, rot.z, rot.w);
            Vector3 pos = Random.insideUnitCircle * generateRadius;

            int rand = Random.Range(0, foodPrefabs.Count);
            GameObject food = Instantiate(foodPrefabs[rand], pos, rot);

            AddFood(food);
        }

        StartCoroutine(GenerateFood());
    }
}
