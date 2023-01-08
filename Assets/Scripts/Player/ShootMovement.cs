using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMovement : MonoBehaviour
{
    private Camera Camera;
    private Player Player;

    private float SunEnergy
    {
        set
        {
            Player?.ChangeSunEnergy(value);
        }
        get
        {
            return Player.Energy;
        }
    }

    public void Init(Player player)
    {
        Player = player;
        Camera = Camera.main;
    }

    [SerializeField]
    private LineRenderer LineRenderer;
    [SerializeField]
    private Arrow ArrowPrefab;


    [SerializeField]
    private Transform LauchPosition;
    public bool IfCanShoot = true;

    [SerializeField]
    private float shootForce = 50f;

    int groundLayerMask = LayerMask.GetMask("Ground");

    void Update()
    {
        if (!IfCanShoot)
            return;

        var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayerMask))
        {
            var hitPoint = hit.point;
            var shootDir = new Vector3(hitPoint.x - LauchPosition.position.x, 0, hitPoint.z - LauchPosition.position.z).normalized;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var arrow = Instantiate<Arrow>(ArrowPrefab);
                arrow.transform.position = LauchPosition.position;
                arrow.Lauch(shootDir, shootForce);
            }
        }

    }
}
