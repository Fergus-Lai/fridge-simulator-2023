using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class KeepBoxInFrame : MonoBehaviour
{
    Camera camera;
    public GameObject target;

    [Range(0.8f, 1.5f)]
    public float PadFactor = 1.1f;

    public float MaxLerpSpeed = 2f;
    public float MoveTime = 0.5f;
    public float RotationSpeed = 0.01f;

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

        float width = bounds.size.x * PadFactor, height = bounds.size.y * PadFactor;
        float gY = Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView);
        float gX = Mathf.Tan(Mathf.Deg2Rad * camera.fieldOfView * camera.aspect);
        float dist = Mathf.Max(width / gX, height / gY);
        dist = Mathf.Max(dist, camera.nearClipPlane);

        Vector3 pos = bounds.center;
        pos.z = bounds.min.z - dist;
        transform.position = SmoothDamp(transform.position, pos, ref smoothVel, MoveTime, MaxLerpSpeed);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(bounds.center - transform.position, Vector3.up),
            1f - Mathf.Pow(0.5f, RotationSpeed * Time.deltaTime));
    }

    static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
    {
        Vector3 res = default;
        res.x = Mathf.SmoothDamp(current.x, target.x, ref currentVelocity.x, smoothTime, maxSpeed);
        res.y = Mathf.SmoothDamp(current.y, target.y, ref currentVelocity.y, smoothTime, maxSpeed);
        res.z = Mathf.SmoothDamp(current.z, target.z, ref currentVelocity.z, smoothTime, maxSpeed);
        return res;
    }
}
