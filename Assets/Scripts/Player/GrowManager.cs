using System;
using System.Collections.Generic;
using System.Xml;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class GrowManager : NetworkBehaviour
{
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private float addScale; //scale for snake resizing
    [SerializeField] private float camAddScale; //scale for camera resizing


    public static event System.Action<ushort> ChangedLengthEvent;

    public NetworkVariable<ushort> length = new NetworkVariable<ushort>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private List<GameObject> tailParts = new List<GameObject>();
    private float curGrowFactor; //range between 0 and 1, if 1 or more add tail part

    private int renderOrder = 0; //order for tail parts
    private CameraResize camScript; //camera resize script

    [HideInInspector] public bool isMultiplyBonus;

    public float TotalScore { get; private set; }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        tailParts.Add(gameObject);
        camScript = Camera.main.GetComponent<CameraResize>();

        if (!IsServer) length.OnValueChanged += OnLengthChanged;

        if(IsOwner) for (int i = 0; i < length.Value - 1; ++i) InstantiateTail();
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        DestroyTails();
    }

    private void DestroyTails()
    {
        foreach (GameObject tail in tailParts)
            Destroy(tail);

        tailParts.Clear();
    }

    public void Eat(float growFactor)
    {
        if (isMultiplyBonus)
        {
            curGrowFactor += growFactor * 2;
            TotalScore += growFactor * 2;
        }
        else
        {
            curGrowFactor += growFactor;
            TotalScore += growFactor;
        }

        if (curGrowFactor < 1) return;
        curGrowFactor = 0;

        AddTail();
    }

    public void AddTail()
    {
        length.Value++;
        LengthChanged();
    }

    private void OnLengthChanged(ushort previousValue, ushort newValue) => LengthChanged();

    private void LengthChanged()
    {
        InstantiateTail();

        if (!IsOwner) return;

        ChangedLengthEvent?.Invoke(length.Value);
    }

    private void InstantiateTail()
    {
        camScript.CameraGrow(camAddScale);

        GameObject tail = Instantiate(tailPrefab);

        tail.GetComponent<TailPart>().target = tailParts[tailParts.Count - 1];
        tail.transform.position = tailParts[tailParts.Count - 1].transform.position;
        tail.transform.localScale = transform.localScale;

        renderOrder -= 1;
        tail.GetComponent<SpriteRenderer>().sortingOrder = renderOrder;

        foreach (GameObject obj in tailParts) 
        {
            Transform trans = obj.transform;
            trans.localScale = new Vector3(
                trans.localScale.x + addScale, 
                trans.localScale.y + addScale, 
                trans.localScale.z);
        }

        tailParts.Add(tail);
    }
}
