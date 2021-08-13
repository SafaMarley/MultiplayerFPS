using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 movementVelocity;
    private Vector3 rotationVelocity;
    private Vector3 thrusterForce;
    private Rigidbody playerRb;
    private float cameraRotationVelocity;
    private float currentCameraRotationOnX;

    [SerializeField] private float cameraRotationLimit = 85f;
    
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

    public void rotateCamera(float cameraRotation)
    {
        cameraRotationVelocity = cameraRotation;
    }
    
    public void ApplyThruster(Vector3 jumpForce)
    {
        thrusterForce = jumpForce;
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

        if (thrusterForce != Vector3.zero)
        {
            playerRb.AddForce(thrusterForce * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    void performRotation()
    {
        playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(rotationVelocity));
        
        currentCameraRotationOnX -= cameraRotationVelocity;
        currentCameraRotationOnX = Mathf.Clamp(currentCameraRotationOnX, -cameraRotationLimit, cameraRotationLimit);

        cam.transform.localEulerAngles = new Vector3(currentCameraRotationOnX, 0f, 0f);
    }
}
