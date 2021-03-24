using UnityEngine;

public class PlataformPoint : MonoBehaviour
{
    [SerializeField] private PlataformPoint targetPoint;
    
    private bool isReached = false;


    public bool WaitForTargetReached()
    {
        return targetPoint.GetIsReached();
    }

    public bool GetIsReached()
    {
        return isReached;
    }

    public Transform GetTargetPointPosition()
    {
        return targetPoint.transform;
    }

    public bool PlataformDistance(Transform plataform)
    {

        if (Vector3.Distance(plataform.position, transform.position) < 1 && isReached == false)
        {  
            isReached = true;
            return true;
        } 

        return false;
    }
}
