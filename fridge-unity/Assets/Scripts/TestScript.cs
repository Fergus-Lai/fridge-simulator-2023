using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Test Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Test Update");
        transform.Rotate(Time.deltaTime * 60f, Time.deltaTime * 90f, Time.deltaTime * 100f);
    }
}
