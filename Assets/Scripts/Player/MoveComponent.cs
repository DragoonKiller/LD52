using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prota.Animation;
using Prota;

public class MoveComponent : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Rigidbody;

    public float Speed = 1f;

    public bool IfCanMove = true;
    
    public SimpleAnimation anim;
    
    public Vector2 recordDir = Vector2Int.zero;
    
    public SimpleAnimationAsset runRight;
    public SimpleAnimationAsset runDown;
    public SimpleAnimationAsset runUp;
    public SimpleAnimationAsset idleRight;
    public SimpleAnimationAsset idleDown;
    public SimpleAnimationAsset idleUp;

    
    private void Start()
    {
        if (Rigidbody == null) Rigidbody = GetComponent<Rigidbody>();
        anim = this.GetComponentInChildren<SimpleAnimation>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        ProcessAnimation(x, y);
        if (!IfCanMove) return;
        ProcessMove(x, y);
    }
    
    void ProcessMove(float x, float y)
    {
        var dir = new Vector3(x, 0, y).normalized;
        Rigidbody.velocity = dir * Speed;
    }
    
    void ProcessAnimation(float x, float y)
    {
        (anim != null).Assert();
        
        if(x == 0 && y != 0)
        {
            if(y > 0) PlayAnim(runUp);
            else PlayAnim(runDown);
        }
        else if(x != 0)
        {
            if(x > 0) PlayAnim(runRight);
            else PlayAnim(runRight, true);
        }
        else // x == 0 && y == 0
        {
            if(recordDir.x == 0 && recordDir.y != 0)
            {
                if(recordDir.y > 0) PlayAnim(idleUp);
                else PlayAnim(idleDown);
            }
            else if(recordDir.x != 0)
            {
                if(recordDir.x > 0) PlayAnim(idleRight);
                else PlayAnim(idleRight, true);
            }
            else
            {
                PlayAnim(idleDown);
            }
        }
        
        if(x != 0 || y != 0) recordDir = new Vector2(x, y);
        
    }
    
    void PlayAnim(SimpleAnimationAsset asset, bool mirror = false)
    {
        anim.mirror = mirror;
        anim.Play(asset);
    }
    
}
