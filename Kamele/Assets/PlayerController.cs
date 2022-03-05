using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform MainCamera;
    [SerializeField] float Speed;
    [SerializeField] float RotationSpeed;
    [SerializeField] float ScrollPower;
    Rigidbody Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //movement
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody.velocity = -transform.forward * Speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Rigidbody.velocity = transform.forward * Speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Rigidbody.velocity = transform.right * Speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rigidbody.velocity = -transform.right * Speed;
        }
        else
        {
            Rigidbody.velocity = new Vector3(0,0,0);
        }
        //Rotation
        if(Input.GetMouseButton(1))
        {
            transform.Rotate(0,RotationSpeed * Input.GetAxis("Mouse X"),0);
        }
        //Zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0f )
        {
            MainCamera.localPosition-=new Vector3(0,0,ScrollPower);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f )
        {
            MainCamera.localPosition+=new Vector3(0,0,ScrollPower);
        }
            

    }
}
