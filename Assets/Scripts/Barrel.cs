using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosion;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Instantiate(explosion, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
