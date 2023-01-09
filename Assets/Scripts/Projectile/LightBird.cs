using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBird : MonoBehaviour, IProjectile
{
    [SerializeField]
    private Rigidbody Rigidbody;
    public void Launch(Vector3 dir, float speed)
    {
        //Debug.Log(dir);
        Rigidbody.AddForce(dir * speed, ForceMode.Impulse);
        var angle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public float LifeTime = 3f;
    private float Timer = 0f;

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > LifeTime)
            Destroy(gameObject);
    }
}
