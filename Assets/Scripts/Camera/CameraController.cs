using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Offset;
    public Transform Target;

    void Awake()
    {
        Offset = transform.position - Target.transform.position;
    }

    void Update()
    {
        transform.position = Target.transform.position + Offset;
    }
}
