using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Content.Interaction;

public class Vehicle : MonoBehaviour
{
    public Transform player;
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
        if (transmissionBox.value < 0.25f)
        {

        }
        else if (transmissionBox.value < 0.5f)
        {

        }
        else if (transmissionBox.value < 0.75f)
        {

        }
        else
        {

        }
        float isMotor = 0;
        if (isPlayerInVehicle && positiveTrigger.action.ReadValue<float>() > 0.5f)
        {
            speed += plusAcceleration * Time.deltaTime;
        }
        else if (isPlayerInVehicle && negativeTrigger.action.ReadValue<float>() > 0.5f)
        {
            speed -= 2 * plusAcceleration * Time.deltaTime;
        }
        else
        {
            if (speed > 0)
            {
                speed -= minusAcceleration * Time.deltaTime;
                speed = Mathf.Max(0, speed);
            }
            else
            {
                speed += minusAcceleration * Time.deltaTime;
                speed = Mathf.Min(0, speed);
            }
            isMotor = 1;
        }
        foreach (var wheel in wheels)
        {
            wheel.Steer(Mathf.Lerp(-1, 1, steeringWheel.value));
            wheel.Accelerate(speed);
            wheel.SetBreakTorque(breakTorque * isMotor);
            wheel.UpdateWheelPosition();
        }
    }
    void Start()
    {
        playerSpeed = continuousMoveProvider.moveSpeed;
    }

    public void SetPlayer()
    {
        isPlayerInVehicle = true;
        player.parent = transform;
        player.position = inPoint.position;
        player.rotation = inPoint.rotation;
        continuousMoveProvider.moveSpeed = 0;
    }
    public void RemovePlayer()
    {
        isPlayerInVehicle = false;
        player.parent = null;
        player.position = outPoint.position;
        player.rotation = outPoint.rotation;
        continuousMoveProvider.moveSpeed = playerSpeed;
    }
}
