using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraResize : MonoBehaviour
{
    [SerializeField] private float resizeSpeed;

    private Camera cam;

    private void Awake() => cam = GetComponent<Camera>();

    public void CameraGrow(float addScale) => StartCoroutine(StartGrow(cam.orthographicSize + addScale));

    public void CameraReGrow(float addScale) => StartCoroutine(StartReGrow(cam.orthographicSize - addScale));

    private IEnumerator StartGrow(float newScale)
    {
        while (cam.orthographicSize < newScale) 
        {
            cam.orthographicSize += Time.deltaTime * resizeSpeed;
            yield return null;
        }
    }

    private IEnumerator StartReGrow(float newScale)
    {
        while(cam.orthographicSize > newScale) 
        { 
            cam.orthographicSize -= Time.deltaTime * resizeSpeed;
            yield return null;
        }
    }
}
