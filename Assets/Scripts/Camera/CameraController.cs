using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Offset;
    public Transform Target;

    void Awake()
    {
        if (Target == null)
            return;
        Offset = transform.position - Target.transform.position;
    }

    void Update()
    {
        if (Target == null)
            return;
        transform.position = Target.transform.position + Offset;
    }
}
