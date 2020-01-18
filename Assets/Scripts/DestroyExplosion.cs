using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, 0.4f);
    }
}
