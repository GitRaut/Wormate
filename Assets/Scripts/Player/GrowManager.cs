using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GrowManager : MonoBehaviour
{
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private int startSize; //start count of tail parts
    [SerializeField] private float addScale; //scale for snake resizing
    [SerializeField] private float camAddScale; //scale for camera resizing

    private List<GameObject> tailParts = new List<GameObject>();
    private float curGrowFactor; //range between 0 and 1, if 1 or more add tail part

    private int renderOrder = 0; //order for tail parts
    private CameraResize camScript; //camera resize script

    [HideInInspector] public bool isMultiplyBonus;

    public float TotalScore { get; private set; }

    private void Start()
    {
        tailParts.Add(gameObject);
        camScript = Camera.main.GetComponent<CameraResize>();
        for (int i = 0; i < startSize; i++) AddTail();
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

    private void AddTail()
    {
        camScript.CameraGrow(camAddScale);

        GameObject tail = Instantiate(tailPrefab);

        tail.GetComponent<TailPart>().target = tailParts[tailParts.Count - 1];
        tail.transform.position = tailParts[tailParts.Count - 1].transform.position;
        tail.transform.localScale = transform.localScale;

        tailParts.Add(tail);
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
    }
}
