using UnityEngine;

public class TailPart : MonoBehaviour
{
    [SerializeField] private float slowSpeed;
    [SerializeField] private float fastSpeed;

    [HideInInspector] public GameObject target;

    private float curSpeed;

    void FixedUpdate()
    {
        if (!target) return;

        if (Input.GetKey(KeyCode.Mouse0)) curSpeed = fastSpeed;
        else curSpeed = slowSpeed;

        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * curSpeed);
    }
}
