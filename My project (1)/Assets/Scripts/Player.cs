using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _jumpKeyWasPressed;
    private float _horizontalInput;
    private Rigidbody _rigidbodyComponent;
    private int superJumpsRemaining;
    [SerializeField] private Transform _groundCheckTransform = null;
    [SerializeField] LayerMask _playerMask;

    void Start()
    {
        _rigidbodyComponent = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpKeyWasPressed = true;
        }

        _horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (_jumpKeyWasPressed && Physics.OverlapSphere(_groundCheckTransform.position, 0.1f, _playerMask).Length != 0)
        {
            float jumpPower = 6f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 1.5f;
                superJumpsRemaining--;
            }
            _rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            _jumpKeyWasPressed = false;
        }
        _rigidbodyComponent.linearVelocity = new Vector3(_horizontalInput, _rigidbodyComponent.linearVelocity.y, _rigidbodyComponent.linearVelocity.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

}
    
