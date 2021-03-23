using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUp : MonoBehaviour
{
    public string powerUpType;
    public float changeValue;
    public int timeEffect;
    private IPowered target;
   

   private void OnTriggerEnter(Collider other) 
   {
       if(other.gameObject.GetComponent<IPowered>() != null)   //Verifica que obtiene el componente Ipowered, sino no hace nada.(null)
       {
           target = other.gameObject.GetComponent<IPowered>();  //Vincula el player con el Ipowered.
           target.changeStat(powerUpType, changeValue, timeEffect); //Efecto que va hacer. 
           Destroy(this.gameObject);    //Forma sencilla de hacer que el objeto se autodestruya.
           

       }
   }
    
}
