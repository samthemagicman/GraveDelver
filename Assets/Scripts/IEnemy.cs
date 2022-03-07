using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemy
{
    float Health {get;set;}

    void Knockback(Vector3 origin, float power = 15);
    void Damage(float damage);
}
