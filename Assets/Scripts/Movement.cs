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
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.Move(move * (Time.deltaTime * speed));
        
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _location.y += Mathf.Sqrt(jumpPower * -jumpHeight * GRAVITY);
        }

        _location.y += GRAVITY * Time.deltaTime;
        _characterController.Move(_location * Time.deltaTime);
    }
    #endregion
}
