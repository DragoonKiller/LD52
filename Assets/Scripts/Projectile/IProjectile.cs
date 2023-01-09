using System;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public void Launch(Vector2 from, Vector2 to, float speed);
}
