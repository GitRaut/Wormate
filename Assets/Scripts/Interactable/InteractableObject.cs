using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class InteractableObject : NetworkBehaviour
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
