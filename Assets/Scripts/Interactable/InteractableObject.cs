using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] protected float lifeTime;

    public void Interact(GameObject obj) => StartCoroutine(StartTimer(obj));

    protected virtual void OnInteract(GameObject obj) { }

    protected virtual void OnDeInteract(GameObject obj) { }

    protected IEnumerator StartTimer(GameObject obj)
    {
        OnInteract(obj);
        yield return new WaitForSeconds(lifeTime);
        OnDeInteract(obj);
    }
}
