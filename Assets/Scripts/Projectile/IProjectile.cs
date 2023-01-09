using System;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public void Launch(Vector3 dir, float speed);
}
