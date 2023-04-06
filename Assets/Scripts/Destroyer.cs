using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollectCube")
        {
            collision.gameObject.GetComponent<Move>().enabled = false;
        }
    }
}
