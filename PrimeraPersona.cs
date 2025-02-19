using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeraPersona : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    [SerializeField] private float _mouseSensitivity = 100f;

    private Vector2 _currentAngle;

    [SerializeField] private float _verticalRotationLimit = 80f;

    void Start()
    {
        offset = transform.position - player.transform.position;

        _currentAngle = new Vector2(transform.eulerAngles.x, player.transform.eulerAngles.y);
        offset = new Vector3(0, 0, 0 * 0);
    }

    void LateUpdate()
    {
        if (player!=null){
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _currentAngle.x -= mouseY * _mouseSensitivity * Time.deltaTime;
        _currentAngle.y += mouseX * _mouseSensitivity * Time.deltaTime;

        _currentAngle.x = Mathf.Clamp(_currentAngle.x, -_verticalRotationLimit, _verticalRotationLimit);

        transform.localRotation = Quaternion.Euler(_currentAngle.x, _currentAngle.y, 0);

        transform.position = player.transform.position + offset;
    }
    }
}
