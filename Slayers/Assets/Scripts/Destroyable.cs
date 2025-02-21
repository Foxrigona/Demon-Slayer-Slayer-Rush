using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public void destroyObstacle()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        if(this.transform.position.x <= -10)
        {
            FindObjectOfType<VillageHealth>().TakeDamage();
            destroyObstacle();
        }
    }
}
