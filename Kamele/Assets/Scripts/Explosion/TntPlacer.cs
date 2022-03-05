using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TntPlacer : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask uiLayer;


    private void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Do nothing
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.GetQuantity() > 0)
            {
                PlaceTnt();
            }
        }
    }

    private void PlaceTnt()
    {
        RaycastHit hitPoint;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity, uiLayer)) return;

        if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity))
        {
            Instantiate(GameManager.Instance.GetCurrentPrefab(), hitPoint.point, Quaternion.identity);
            GameManager.Instance.DecreaseQuantity();
            HUDManager.Instance.UpdateTMP();
        }
    }
}
