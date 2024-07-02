using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    public float objectHealth = 10.0f;

    public void ObjectHitDamage(float amount)
    {
        objectHealth -= amount;
        if(objectHealth <= 0.0f)
            Destroy(gameObject);
    }
}
