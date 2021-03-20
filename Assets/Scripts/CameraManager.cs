using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothMovement = 1f;
    [SerializeField] private float rotationDegrees = 10f;
    
    private Space offsetPositionSpace = Space.Self;

    private Vector3 followPosition;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        followPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            target.position.z + offset.z);
        
        // transform.rotation = Quaternion.LookRotation( followPosition - transform.position, -Physics.gravity ) * 
        //                      Quaternion.Euler( Input.GetAxis("Vertical") * rotationDegrees, Input.GetAxis("Horizontal") * rotationDegrees, 0f );
    
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            followPosition, 
            ref velocity, 
            smoothMovement);
    }

    public void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        
        // if (offsetPositionSpace == Space.Self)
        // {
        //     transform.position = target.TransformPoint(offset);
        // }
        // else
        // {
        //     transform.position = target.position + offset;
        // }
        
        transform.position = target.TransformPoint(offset);
        
        transform.LookAt(target);
        
    }
}