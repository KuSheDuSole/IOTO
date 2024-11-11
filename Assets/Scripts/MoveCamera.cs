using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    
    [SerializeField]
    private float _mouseSensitivity = 3.0f;
    private float _rotationY;
    private float _rotationX;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _distanceFromTarget = 3.0f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);
    private Rigidbody _rigidbody;
    
    [SerializeField]
    private float _moveSpeed = 5.0f; // Скорость движения объекта
    private void Start() {
        _rigidbody = _target.GetComponent<Rigidbody>();
    }
    void Update()
    {
        // Получаем движения мыши
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        // Обновляем углы поворота
        _rotationY += mouseX;
        _rotationX -= mouseY;

        // Ограничиваем угол по вертикали
        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

        // Вектор следующего поворота
        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        // Сглаживаем поворот камеры
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        // Устанавливаем положение камеры
        transform.position = _target.position - transform.forward * _distanceFromTarget;

        // Обработка движения объекта
        MoveTarget();
    }
    private void MoveTarget()
    {
        // Получаем векторы движения по осям X и Z
        float horizontal = Input.GetAxis("Horizontal"); // A/D или стрелки влево/вправо
        float vertical = Input.GetAxis("Vertical"); // W/S или стрелки вверх/вниз

        // Направление движения относительно камеры
        Vector3 moveDirection = (transform.forward * vertical + transform.right * horizontal).normalized;

        // Двигаем объект
        //_target.position += moveDirection * _moveSpeed * Time.deltaTime;
        _rigidbody.AddForce(moveDirection);
    }
    
}
