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

    [SerializeField] XRSlider transmissionBox;

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
        
    }
    void Start()
    {
        playerSpeed = continuousMoveProvider.moveSpeed;
    }

    public void SetPlayer()
    {
        isPlayerInVehicle = true;
        characterController.enabled = false;
        player.parent = transform;
        player.position = inPoint.position;
        player.rotation = inPoint.rotation;
        continuousMoveProvider.moveSpeed = 0;
    }
    [ContextMenu("RemovePlayer")]
    public void RemovePlayer()
    {
        Debug.Log("R");
        isPlayerInVehicle = false;
        characterController.enabled = true;
        player.parent = null;
        player.position = outPoint.position;
        player.rotation = outPoint.rotation;
        continuousMoveProvider.moveSpeed = playerSpeed;
    }
}
