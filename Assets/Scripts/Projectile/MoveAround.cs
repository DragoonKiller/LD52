using UnityEngine;
using Prota;
using Prota.Unity;

public class MoveAround : MonoBehaviour
{
    public Transform _target;
    Transform target => _target == null ? this.transform.parent : _target;
    
    public float rotSpeed = 0.5f;
    
    void Update()
    {
        var p = this.transform.localPosition;
        // to xz
        var pos = new Vector2(p.x, p.z);
        pos = pos.Rotate(Time.deltaTime * rotSpeed);
        this.transform.localPosition = new Vector3(pos.x, p.y, pos.y);
    }
    
    
}