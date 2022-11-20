using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles scooting in and out and aligns its children to a grid
/// </summary>
public class ShelfScript : MonoBehaviour, IPointerClickHandler
{
    bool isExtended = false;
    private Vector3 initialPos;
    public Vector3 extension;
    public float extendTime = 0.5f;
    public float floorOffset = 0.05f;
    public int gridX = 10;
    public int gridZ = 4;
    public string shelfName;

    private bool _isFocused;
    public bool IsFocused
    {
        get => _isFocused;
        set
        {
            if (_isFocused != value)
            {
                _isFocused = value;
                StopAllCoroutines();
                StartCoroutine(AnimateSlide(value ? extension : Vector3.zero, extendTime));
            }
        }
    }

    void Start()
    {
        initialPos = transform.localPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FridgeManager.Instance.ShelfClicked(this);
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
