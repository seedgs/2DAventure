using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarRun : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<EnemyController>()?.boarRun();
        //Debug.Log("111111111");
    }
}
