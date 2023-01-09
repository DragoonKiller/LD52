using System.Collections;
using System.Collections.Generic;
using Prota;
using Prota.Unity;
using UnityEngine;

public class HarvestBird : MonoBehaviour, IProjectile
{
    [SerializeField]
    private Rigidbody Rigidbody;
    public void Launch(Vector2 from, Vector2 to, float speed)
    {
        var dir = from.To(to);
        
        Rigidbody.velocity = new Vector3(dir.x, 0, dir.y).normalized * speed;
        var angle = Vector2.SignedAngle(dir, Vector2.up);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        
        this.transform.position = new Vector3(from.x, 0, from.y);
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
