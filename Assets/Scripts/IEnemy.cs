using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemy
{
    void Knockback(Vector3 origin, float power = 15);
}
