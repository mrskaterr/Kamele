using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntPlacer : MonoBehaviour
{
    [SerializeField] 
    private GameObject tnt;

    [SerializeField]
    private Camera cam;
    

    private Vector3 mousePos;
    private Vector3 tempPos;

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTnt();
        }
    }

    private void PlaceTnt()
    {
        RaycastHit hitPoint;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity))
        {
          Instantiate(tnt, hitPoint.point, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(mousePos, tempPos);
    }
}
