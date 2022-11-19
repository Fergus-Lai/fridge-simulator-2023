using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShelfScript : MonoBehaviour, IPointerClickHandler
{
    public KeepBoxInFrame cameraController;
    public GameObject mainFridge;
    bool isExtended = false;
    private Vector3 initialPos;
    public Vector3 extension;
    public float extendTime = 0.5f;

    void Start()
    {
        initialPos = transform.localPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isExtended)
        {
            JSBindings.AddItem("test");
            cameraController.target = this.gameObject;
            cameraController.direction = ViewDirection.Top;
            isExtended = true;
            StartCoroutine(AnimateSlide(extension, extendTime));
        }
        else
        {
            cameraController.target = mainFridge;
            cameraController.direction = ViewDirection.Front;
            isExtended = false;
            StartCoroutine(AnimateSlide(Vector3.zero, extendTime));
        }
    }

    private IEnumerator AnimateSlide(Vector3 pos, float t)
    {
        pos += initialPos;
        Vector3 start = transform.localPosition;
        float et = Time.time + t;
        while (et > Time.time)
        {
            transform.localPosition = Vector3.Lerp(start, pos, Mathf.SmoothStep(1f, 0f, (et - Time.time) / t));
            yield return null;
        }

        transform.localPosition = pos;
    }
}
