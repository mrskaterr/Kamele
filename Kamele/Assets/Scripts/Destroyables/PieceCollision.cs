using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Undestroyable")) return;

        if (collision.collider.CompareTag("Building"))
        {
            PointsManager.Instance.AddPoints(collision.collider.GetComponent<Building>().GetPoints());
            collision.collider.GetComponent<Building>().SetDestroyedState();
        }
        if (!collision.collider.gameObject.GetComponent<Rigidbody>())
        {
            collision.collider.gameObject.AddComponent<Rigidbody>();
        }
    }
}
