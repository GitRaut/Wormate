using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject head;
    [Space(10)]
    [Header("Move Settings")]
    [SerializeField] private float slowSpeed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float rotateSpeed;

    private Transform player;
    private Camera cam;
    private FollowTarget camScript;
    private Vector3 target;
    private float curSpeed;

    private void Init()
    {
        cam = Camera.main;
        camScript = cam.GetComponent<FollowTarget>();

        curSpeed = slowSpeed;
        player = transform.parent;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Init();
    }

    private void FixedUpdate()
    {
        if (!IsOwner || !Application.isFocused) return;

        if (Input.GetKey(KeyCode.Mouse0)) curSpeed = fastSpeed;
        else curSpeed = slowSpeed;

        Move();
    }

    private void Move()
    {
        target = cam.ScreenToWorldPoint(Input.mousePosition);

        float movementSpeed = Time.deltaTime * curSpeed;

        float angle = Mathf.Atan2(target.y - head.transform.position.y, target.x - head.transform.position.x) * Mathf.Rad2Deg;
        Quaternion qTo = Quaternion.Euler(new Vector3(0, 0, angle));
        head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, qTo, rotateSpeed * Time.fixedDeltaTime);

        Vector3 vec = head.transform.up;
        player.Translate(vec * movementSpeed);

        camScript.Follow(player);
    }
}
