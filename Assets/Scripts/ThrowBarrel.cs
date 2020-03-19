using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBarrel : MonoBehaviour
{
    public GameObject barrel;
    public Transform barrelPos;
    public Vector2 barrelDirection;
    private GameObject tmpBarrel;

    void Start()
    {
        StartCoroutine("createBarrel");
    }

    IEnumerator createBarrel()
    {
        tmpBarrel = Instantiate(barrel, barrelPos.position, Quaternion.identity);
        tmpBarrel.transform.position = barrelPos.position;
        tmpBarrel.transform.rotation = Quaternion.identity;
        tmpBarrel.GetComponent<Rigidbody2D>().AddForce(barrelDirection);

        //From object pool
        // tmpBarrel = ObjectPool.instance.GetGameObjectOfType("barrel_0", true);
        // if (tmpBarrel != null)
        // {
        //     tmpBarrel.transform.position = barrelPos.position;
        //     tmpBarrel.transform.rotation = Quaternion.identity;
        //     tmpBarrel.GetComponent<Rigidbody2D>().AddForce(barrelDirection);
        // }

        yield return new WaitForSeconds(3f);
        StartCoroutine("createBarrel");
    }
}
