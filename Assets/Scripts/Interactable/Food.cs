using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : InteractableObject
{
    [SerializeField] private float growFactor;

    protected override void OnInteract(GameObject obj)
    {
        obj.GetComponent<GrowManager>().Eat(growFactor);
        FoodManager.instance.RemoveFood(gameObject);
        Destroy(gameObject);
    }
}
