using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    
    private PlayerMotor motor;
    
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //Calculate movement
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMovement;
        Vector3 moveVertical = transform.forward * zMovement;

        Vector3 movingVector = (moveHorizontal + moveVertical).normalized * speed;
        motor.move(movingVector);
        
        //Calculate rotation
        float yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 yRotationVector = new Vector3(0f, yRotation, 0f) * lookSensitivity;

        motor.rotate(yRotationVector);
        
        //Calculate camera rotation
        float xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotationVector = new Vector3(xRotation, 0f, 0f) * lookSensitivity;

        motor.rotateCamera(cameraRotationVector);
    }
}
