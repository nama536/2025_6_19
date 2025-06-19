using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject candy;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CandyPop();
        }
    }

    public void CandyPop()
    {
        GameObject candyPrefab = Instantiate(candy, transform);

        candyPrefab.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }
}
