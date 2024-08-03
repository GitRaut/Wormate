using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBonus : InteractableObject
{
    [SerializeField] private float addScale;

    protected override void OnInteract(GameObject obj)
    {
        Camera.main.GetComponent<CameraResize>().CameraGrow(addScale);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    protected override void OnDeInteract(GameObject obj)
    {
        Camera.main.GetComponent<CameraResize>().CameraReGrow(addScale);
        Destroy(gameObject);
    }
}
