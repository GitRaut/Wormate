using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyBonus : InteractableObject
{
    protected override void OnInteract(GameObject obj)
    {
        obj.GetComponent<GrowManager>().isMultiplyBonus = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    protected override void OnDeInteract(GameObject obj)
    {
        obj.GetComponent<GrowManager>().isMultiplyBonus = false;
        Destroy(gameObject);
    }
}
