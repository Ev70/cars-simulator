using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Content.Interaction;

public class Vehicle : MonoBehaviour
{
    public Transform player;
    [SerializeField] CharacterController characterController;
    [SerializeField] ContinuousMoveProviderBase continuousMoveProvider;
    float playerSpeed;
    public bool isPlayerInVehicle;
    [SerializeField] float breakTorque;

    [SerializeField] Transform inPoint;
    [SerializeField] Transform outPoint;

    [SerializeField] XRKnob steeringWheel;
    [SerializeField] Wheel[] wheels;

    [SerializeField] float speed;
    [SerializeField] float plusAcceleration = 10;
    [SerializeField] float minusAcceleration = 10;
    [SerializeField] InputActionProperty positiveTrigger;
    [SerializeField] InputActionProperty negativeTrigger;

    [SerializeField] Speedometer speedometer;

    [SerializeField] XRSlider transmissionBox;
    Rigidbody rb;

    [SerializeField] AudioSource transmissionChangeSound;
    [SerializeField] AudioSource motorSound;
    [SerializeField] AudioSource backMoving;
    [SerializeField] AudioSource enviromentSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSpeed = continuousMoveProvider.moveSpeed;
    }
    void Update()
    {
        if (isPlayerInVehicle)
        {
            if (transmissionBox.value < 0.25f)
            {
                foreach (var wheel in wheels)
                {
                    wheel.Steer(Mathf.Lerp(-1, 1, steeringWheel.value));
                    wheel.Accelerate(0);
                    wheel.SetBreakTorque(breakTorque);
                    wheel.UpdateWheelPosition();
                }
            }
            else if (transmissionBox.value < 0.5f)
            {
                
                float isNotMotor = 0;
                if (isPlayerInVehicle && positiveTrigger.action.ReadValue<float>() > 0.5f)
                {
                    speed -= plusAcceleration * Time.deltaTime;
                }
                else if (isPlayerInVehicle && negativeTrigger.action.ReadValue<float>() > 0.5f)
                {
                    speed += plusAcceleration * Time.deltaTime;
                    isNotMotor = 5;
                }
                else
                {
                    speed += minusAcceleration * Time.deltaTime;
                    isNotMotor = 1;
                }
                speed = Mathf.Min(0, speed);
                foreach (var wheel in wheels)
                {
                    wheel.Steer(Mathf.Lerp(-1, 1, steeringWheel.value));
                    wheel.Accelerate(speed);
                    wheel.SetBreakTorque(breakTorque * isNotMotor);
                    wheel.UpdateWheelPosition();
                }
            }
            else if (transmissionBox.value < 0.75f)
            {
                foreach (var wheel in wheels)
                {
                    wheel.Steer(Mathf.Lerp(-1, 1, steeringWheel.value));
                    wheel.Accelerate(speed);
                    wheel.UpdateWheelPosition();
                }
            }
            else
            {
                float isNotMotor = 0;
                if (isPlayerInVehicle && positiveTrigger.action.ReadValue<float>() > 0.5f)
                {
                    speed += plusAcceleration * Time.deltaTime;
                }
                else if (isPlayerInVehicle && negativeTrigger.action.ReadValue<float>() > 0.5f)
                {
                    speed -= plusAcceleration * Time.deltaTime;
                    isNotMotor = 5;
                }
                else
                {
                    speed -= minusAcceleration * Time.deltaTime;
                    isNotMotor = 1;
                }
                speed = Mathf.Max(0, speed);
                foreach (var wheel in wheels)
                {
                    wheel.Steer(Mathf.Lerp(-1, 1, steeringWheel.value));
                    wheel.Accelerate(speed);
                    wheel.SetBreakTorque(breakTorque * isNotMotor);
                    wheel.UpdateWheelPosition();
                }
            }
        }
        if (speedometer) speedometer.speed = Mathf.Abs(rb.velocity.magnitude*3);
    }

    public void SetPlayer()
    {
        isPlayerInVehicle = true;
        if (characterController) characterController.enabled = false;
        
        player.position = inPoint.position;
        player.rotation = inPoint.rotation;
        continuousMoveProvider.moveSpeed = 0;
        player.parent = transform;
        motorSound.Play();
        enviromentSound.volume = 0.5f;

    }
    [ContextMenu("RemovePlayer")]
    public void RemovePlayer()
    {
        isPlayerInVehicle = false;
        
        player.parent = null;
        player.position = outPoint.position;
        player.rotation = outPoint.rotation;
        continuousMoveProvider.moveSpeed = playerSpeed;
        if (characterController) characterController.enabled = true;
        motorSound.Stop();
        enviromentSound.volume = 1;
    }
    public void SetTransmissionBoxKnobPosition()
    {
        transmissionChangeSound.Play();
        if (transmissionBox.value < 0.25f)
        {
            transmissionBox.value = 0;
            backMoving.Stop();
        }
        else if (transmissionBox.value < 0.5f)
        {
            transmissionBox.value = 0.33f;
            backMoving.Play();
        }
        else if (transmissionBox.value < 0.75f)
        {
            transmissionBox.value = 0.66f;
            backMoving.Stop();
        }
        else
        {
            transmissionBox.value = 1;
            backMoving.Stop();
        }
    }
}
