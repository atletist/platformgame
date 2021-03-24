using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private PlataformPoint[] points;
    [SerializeField] private float speed;
    private Vector3 velocity = Vector3.zero;

    private void Update() 
    {

        if(!points[0].PlataformDistance(transform))
        {
            transform.position = Vector3.SmoothDamp(transform.position, points[0].transform.position, ref velocity, speed);
        }

        /*
        if (Vector3.Distance(transform.position, points[0].position) > 0 && goBack == false)
        {
            transform.position = Vector3.SmoothDamp(transform.position, points[0].position, ref velocity, speed);
        }

        if (Vector3.Distance(transform.position, points[0].position) < 1)
        {
            goBack = true;
        }

        if (Vector3.Distance(transform.position, startPosition) > 0 && goBack == true)
        {
             transform.position = Vector3.SmoothDamp(transform.position, startPosition, ref velocity, speed);
        }
        */
        

        
    }

}