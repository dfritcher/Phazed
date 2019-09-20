using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableColliders : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player")
            return;

        //Debug.Log("<color=#06A250> Enable Colliders.</color>");
        ColliderManager.EnableColliders();
    }
}
