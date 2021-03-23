using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPowered
{
 

 //Declaración de las variables necesarias para el movimiento.
    float horizontalMove;
    float verticalMove;
    private Vector3 playerInput;
    

    public CharacterController player;
    public float playerSpeed;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    public Camera mainCamera;
     //Direcciones de la cámara.
    private Vector3 camForward;
    private Vector3 camRight;  
    private Vector3 movePlayer;   //Hacia dónde se va a mover nuestro personaje , donde mira.

    public bool isGrounded;

    //Variables deslizamiento en pendientes
    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;
  

    // Start is called before the first frame update
    void Start()
    {
        // Aquí llamamos al componente Charactercontroller del inspector.
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        // Movimientos en horizontal y vertical , siendo Axis los controles predefinidos como w,a,s,d y un joystick.

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        CamDirection();

        //Función para que siempre se mueva la cámara respecto al jugador.

        movePlayer = playerInput.x * camRight + playerInput.z * camForward; 
        movePlayer = movePlayer * playerSpeed;

        // Hace que el personaje mire hacia la dirección a la que avanzas.

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        // Mejora del movimiento para que vaya con el tiempo del juego (delta time)
        player.Move(movePlayer * Time.deltaTime);

        //Función para las habilidades del jugador. (Saltos, ataques, agarrar...)

        SetGravity();


        PlayerSkills();

    }

    void CamDirection()
    {
    camForward = mainCamera.transform.forward;
    camRight = mainCamera.transform.right;

    camForward.y = 0;
    camRight.y = 0; 

    //Esto nos dará el valor normalizado hacia donde mira la cam.
    camForward = camForward.normalized;
    camRight = camRight.normalized;  

    }

    
    public void SetGravity()
    {
        

        if (player.isGrounded)
        {
           // fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity; 
            isGrounded = true;

        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
         SlideDown(); //Llamamos a la funcion SlideDown() para comprobar si estamos en una pendiente

        
    }

    public void SlideDown()
    {
        //si el angulo de la pendiente en la que nos encontramos es mayor o igual al asignado en player.slopeLimit, isOnSlope es VERDADERO
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
        if (isOnSlope) //Si isOnSlope es VERDADERO
        {
            //movemos a nuestro jugador en los ejes "x" y "z" mas o menos deprisa en proporcion al angulo de la pendiente.
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;
            //y aplicamos una fuerza extra hacia abajo para evitar saltos al caer por la pendiente.
            movePlayer.y += slopeForceDown;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Almacenamos la normal del plano contra el que hemos chocado en hitNormal.
        hitNormal = hit.normal;
    }

    public void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }

    }

    public void changeStat(string powerUpName, float powerUpValue, int powerUptTime)   //Contrato con el Ipowered, se hizo dandole a la bombilla.
    {
          if(powerUpName == "Jump")
          {
              StartCoroutine(PowerUpCounter(powerUpValue, powerUptTime));  //No abusar de las corutinas. Colapsan.
         
          }
    }
    //Corrutinas.
    IEnumerator PowerUpCounter ( float powerUpValue, int powerUptTime)
    {
        float tempJumpForce = jumpForce;  // Asignar el valor inicial , para poder volver a retomarlo luego. (Lo mantiene)
        jumpForce = powerUpValue;
        yield return new WaitForSeconds(powerUptTime);   //Crea un contador, espera x segundos.
        jumpForce = tempJumpForce;
    }
}
