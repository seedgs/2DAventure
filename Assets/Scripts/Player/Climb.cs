using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<PlayerController>().PlayerClimb();
        //Debug.Log(collision.name);
    }
}
