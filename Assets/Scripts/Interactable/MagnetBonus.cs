using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBonus : InteractableObject
{
    protected override void OnInteract(GameObject obj)
    {
        obj.GetComponentInParent<PointEffector2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    protected override void OnDeInteract(GameObject obj)
    {
        obj.GetComponentInParent<PointEffector2D>().enabled = false;
        Destroy(gameObject);
    }
}
