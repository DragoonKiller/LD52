using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Rigidbody;

    public float Speed = 1f;

    public bool IfCanMove = true;

    private float InputX, InputY;

    private void Start()
    {
        if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IfCanMove)
            return;

        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
        var dir = new Vector3(InputX, 0, InputY).normalized;
        Rigidbody.velocity = dir * Speed;
    }
}
