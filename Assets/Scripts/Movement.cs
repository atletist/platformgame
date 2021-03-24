using UnityEngine;

public class Movement : MonoBehaviour
{
    private const float GRAVITY = -9.8f * 2;

    private CharacterController _characterController;
    private Vector3 _location;

    [Header("Movement values")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float smoothRotation;

    private float smoothRotationVelocity;

    #region Class Logic
    #endregion

    #region MonoBehaviour API

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = _characterController.isGrounded;

        if (isGrounded && _location.y < 0)
        {
            _location.y = 0f;
        }
        
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float characterAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, characterAngle, ref smoothRotationVelocity, smoothRotation);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 move = Quaternion.Euler(0f, characterAngle, 0f) * Vector3.forward;
            
            _characterController.Move(move.normalized * (Time.deltaTime * speed));
        }

        // if (direction != Vector3.zero)
        // {
        //     gameObject.transform.forward = direction;
        // }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _location.y += Mathf.Sqrt(jumpPower * -jumpHeight * GRAVITY);
        }

        _location.y += GRAVITY * Time.deltaTime;
        _characterController.Move(_location * Time.deltaTime);
    }
    #endregion
}