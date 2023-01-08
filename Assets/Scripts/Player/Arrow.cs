using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Rigidbody;
    public void Lauch(Vector3 dir, float speed)
    {
        //Debug.Log(dir);
        transform.LookAt(transform.position + dir, Vector3.forward);
        Rigidbody.AddForce(dir * speed, ForceMode.Impulse);
    }

    public float LifeTime = 3f;
    private float Timer = 0f;

    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > LifeTime)
            Destroy(gameObject);
    }
}
