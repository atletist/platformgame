using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothMovement = 1f;

    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      var followPosition = new Vector3(
          target.position.x + offset.x,
          target.position.y + offset.y,
          target.position.z + offset.z);

          transform.position = Vector3.SmoothDamp(
              transform.position,
              followPosition,
              ref velocity,
              smoothMovement);
          


          
    }
    }
