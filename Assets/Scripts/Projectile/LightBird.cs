using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prota.Tween;
using Prota.Unity;
using Prota.Timer;
using Prota;

public class LightBird : MonoBehaviour, IProjectile
{
    bool reachedTarget = false;
    
    public float LifeTime = 5f;
    
    public LightSource lightSource;
    
    public GameObject decos;
    
    [SerializeField]
    private Rigidbody Rigidbody;
    
    public void Launch(Vector2 from, Vector2 to, float speed)
    {
        var dir = from.To(to);
        var angle = Vector2.SignedAngle(dir, Vector2.up);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        
        var flyTime = (to - from).magnitude / speed;
        
        this.transform.position = new Vector3(from.x, 0, from.y);
        this.transform.TweenMoveX(to.x, flyTime);
        this.transform.TweenMoveZ(to.y, flyTime);
        
        Timer.New(flyTime, this.gameObject.LifeSpan(), () => {
            
            decos.gameObject.SetActive(true);
            lightSource.gameObject.SetActive(true);
            decos.transform.rotation = Quaternion.identity;        // in world space
            
            Timer.New(LifeTime, this.gameObject.LifeSpan(), () => {
                this.gameObject.Destroy();
            });
        });
        
    }

    
    void Update()
    {
        if(!reachedTarget) return;
    }
}
