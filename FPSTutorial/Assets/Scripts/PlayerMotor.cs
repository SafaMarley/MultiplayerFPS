using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 movementVelocity;
    private Vector3 rotationVelocity;
    private Vector3 cameraRotationVelocity;
    private Rigidbody playerRb;
    
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void move(Vector3 velocity)
    {
        movementVelocity = velocity;
    }

    public void rotate(Vector3 rotation)
    {
        rotationVelocity = rotation;
    }

    public void rotateCamera(Vector3 cameraRotation)
    {
        cameraRotationVelocity = cameraRotation;
    }
    
    void FixedUpdate()
    {
        performMovement();
        performRotation();
    }

    void performMovement()
    {
        if (movementVelocity != Vector3.zero)
        {
            playerRb.MovePosition(playerRb.position + movementVelocity * Time.fixedDeltaTime);
        }
    }

    void performRotation()
    {
        playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(rotationVelocity));
        cam.transform.Rotate(-cameraRotationVelocity);
    }
}
