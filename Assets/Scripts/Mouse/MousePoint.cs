using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    private Camera Camera;
    public static Vector3 Position;
    public Color CanColor;
    public Color DisColor;

    [SerializeField]
    private SpriteRenderer Sprite;

    int groundLayerMask;
    void Awake()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        Camera = Camera.main;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayerMask))
        {
            Position = hit.point;
            transform.position = Position;
            var player = World.Instance.Player;
            bool ifCanPlant = player.PlantMovement.IfCanPlant;
           
            if (ifCanPlant)
            {
                Sprite.color = CanColor;
            }
            else
            {
                Sprite.color = DisColor;
            }
        }
    }

}
