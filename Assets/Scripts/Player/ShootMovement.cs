using System.Collections;
using System.Collections.Generic;
using Prota;
using Prota.Unity;
using UnityEngine;

public class ShootMovement : MonoBehaviour
{
    private Camera Camera;
    private Player Player;
    
    public ParticleSystem launchParticle;
    
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
    private HarvestBird HarvestBirdPrefab;
    [SerializeField]
    private LightBird LightBirdPrefab;

    [SerializeField]
    private Transform LauchPosition;
    public bool IfCanShoot = true;

    public float recoil = 3f;

    [SerializeField]
    private float projectileSpeed = 15f;

    int groundLayerMask;

    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

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
                var itemPlane = ItemPlane.Instance;

                IProjectile bird = null;

                switch (itemPlane.FocusItem)
                {
                    case ItemType.HarvestBird:
                        if (itemPlane.CostItem(itemPlane.FocusIndex, 1))
                            bird = Instantiate<HarvestBird>(HarvestBirdPrefab, LauchPosition.position, Quaternion.identity);
                        break;
                    case ItemType.LightBird:
                        if (itemPlane.CostItem(itemPlane.FocusIndex, 1))
                            bird = Instantiate<LightBird>(LightBirdPrefab, LauchPosition.position, Quaternion.identity);
                        break;
                    default:
                        bird = null;
                        break;
                }

                if (bird != null)
                {
                    var from = new Vector2(LauchPosition.position.x, LauchPosition.position.z);
                    var to = new Vector2(hitPoint.x, hitPoint.z);
                    bird.Launch(from, to, projectileSpeed);
                    
                    var dir = -from.To(to).normalized;
                    World.Instance.Player.MoveComponent.pulsar = dir * recoil;
                    
                    launchParticle.transform.rotation = Quaternion.Euler(0, Vector2.SignedAngle(-dir, Vector2.up), 0);
                    launchParticle.Emit(30);
                }
            }
        }

    }
}
