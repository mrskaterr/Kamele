using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TntPlacer : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] 
    private LayerMask shaderLayerToIgnore;


    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Do nothing
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.CheckIfTntCanBePlaced())
            {
                PlaceTnt();
            }
        }
    }

    private void PlaceTnt()
    {
        RaycastHit hitPoint;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity))
        {
            Debug.Log(hitPoint);
            Instantiate(GameManager.Instance.GetCurrentPrefab(), hitPoint.point, Quaternion.identity);
            GameManager.Instance.DecreaseQuantity();
            HUDManager.Instance.UpdateTMP();
        }
    }
}
