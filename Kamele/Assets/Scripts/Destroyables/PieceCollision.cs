using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var huj = collision.collider;
        if (huj.CompareTag("Undestroyable")) return;

        if (huj.CompareTag("Building"))
        {
            huj.GetComponent<BuildingsHealthSystem>().DealDamage();
            GameManager.Instance.HitBuilding(collision.collider);
        }
        else if (!huj.gameObject.GetComponent<Rigidbody>())
        {
            huj.gameObject.AddComponent<Rigidbody>();
            huj.gameObject.GetComponent<Rigidbody>().mass = 5f;
        }
    }
}
