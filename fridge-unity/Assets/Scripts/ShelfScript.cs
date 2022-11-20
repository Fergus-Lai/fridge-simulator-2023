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
    public float floorOffset = 0.05f;
    public int gridX = 10;
    public int gridZ = 4;

    private List<GameObject> objects;

    void Start()
    {
        initialPos = transform.localPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isExtended)
        {
            //JSBindings.AddItem("test");
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

    [ContextMenu("Align Children")]
    public void AlignChildren()
    {
        int i = 0;
        foreach (Transform ch in this.transform)
        {
            AlignToGrid(ch, i++);
        }
    }

    public void AlignToGrid(Transform t, int idx)
    {
        int row = idx % gridZ, col = idx / gridZ;
        Debug.Log($"{idx} = {row}, {col}");
        if (col >= gridX) Debug.LogError("The whole world will now explode");
        var bc = this.GetComponent<BoxCollider>();
        float xSize = bc.size.y;
        float zSize = bc.size.x;
        
        float xInc = xSize / gridX, zInc = zSize / gridZ;
        Vector3 pos = bc.center - (bc.size * 0.5f);
        pos.z += floorOffset;
        pos.x += zInc * (0.5f + row);
        pos.y += xInc * (0.5f + col);
        t.localPosition = pos;
    }
}
