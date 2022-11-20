using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class KeepBoxInFrame : MonoBehaviour
{
    private new Camera camera;
    public GameObject target;

    [Range(0.8f, 1.5f)]
    public float PadFactor = 1.1f;

    public float MaxLerpSpeed = 2f;
    public float MoveTime = 0.5f;
    public float RotationSpeed = 0.01f;
    public ViewAngle angle;
    public ViewDirection direction;
    private Vector3 smoothVel;
    void Start ()
    {
        camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Bounds bounds = new(target.transform.position, Vector3.zero);
        foreach (var renderer in target.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds.min);
            bounds.Encapsulate(renderer.bounds.max);
        }

        foreach (var collider in target.GetComponentsInChildren<Collider>())
        {
            bounds.Encapsulate(collider.bounds.min);
            bounds.Encapsulate(collider.bounds.max);
        }

        Quaternion rotation = direction switch {
            ViewDirection.Front => Quaternion.identity,
            ViewDirection.Top => Quaternion.LookRotation(Vector3.down, Vector3.left),
            _ => Quaternion.identity,
        };

        (float width, float height) = direction switch {
            ViewDirection.Front => (bounds.size.x, bounds.size.y),
            ViewDirection.Top => (bounds.size.z, bounds.size.x),
            _ => (bounds.size.x, bounds.size.y),
        };

        float gY = Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView);
        float gX = Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView * camera.aspect);
        float dist = Mathf.Max(width / gX, height / gY);
        dist = Mathf.Max(dist, camera.nearClipPlane);

        Vector3 pos = bounds.center;

        if (angle.HasFlag(ViewAngle.Top)) pos.y = bounds.max.y;
        else if (angle.HasFlag(ViewAngle.Bottom)) pos.y = bounds.min.y;

        if (angle.HasFlag(ViewAngle.Left)) pos.x = bounds.min.x;
        else if (angle.HasFlag(ViewAngle.Right)) pos.x = bounds.max.x;

        if (direction == ViewDirection.Front)
        {
            pos.z = bounds.min.z - dist;
        }
        else if (direction == ViewDirection.Top)
        {
            pos.y = bounds.max.y + dist;
        }

        Vector3 up = direction switch {
            ViewDirection.Front => Vector3.up,
            ViewDirection.Top => Vector3.left,
            _ => Vector3.up,
        };

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref smoothVel, MoveTime, MaxLerpSpeed);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(bounds.center - transform.position, up),
            1f - Mathf.Pow(0.5f, RotationSpeed * Time.deltaTime));
    }
}

[System.Flags]
public enum ViewAngle
{
    Center = 0,
    Top = 1 << 0,
    Bottom = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
}

public enum ViewDirection
{
    Front, Top
}