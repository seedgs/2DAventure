using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarRunColliderCheck : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Boar>())
        {
            collision.GetComponent<Boar>()?.Run();
        }
    }
}
