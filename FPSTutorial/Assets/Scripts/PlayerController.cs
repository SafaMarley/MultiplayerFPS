using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private float thrusterForce = 1000f;

    [Header("Spring Settings:")]
    [SerializeField] private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField] private float jointSpring = 20f;
    [SerializeField] private float jointMaxForce = 40f;
    
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        
        setJointSettings(jointSpring);
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

        float cameraRotationVector = xRotation * lookSensitivity;

        motor.rotateCamera(cameraRotationVector);

        Vector3 jumpForce = Vector3.zero;
        //Apply thruster
        if (Input.GetButton("Jump"))
        {
            jumpForce = Vector3.up * thrusterForce;
            setJointSettings(0f);
        }
        else
        {
            setJointSettings(jointSpring);
        }

        motor.ApplyThruster(jumpForce);
    }

    private void setJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
