using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    public void Follow(Transform target) => transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, offset.z), 1360);
}
