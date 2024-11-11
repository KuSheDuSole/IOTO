using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphere : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce =10.0f;
    [SerializeField]
    private bool _isGrounded = true;
    private Rigidbody _rigidbody;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) Jump();
    }
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false; // Устанавливаем, что сфера не на земле
    }
    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
    }
}
