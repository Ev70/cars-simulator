using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] float maxAngle = 60f;
    [SerializeField] WheelCollider wheelCollider;
    [SerializeField] Transform wheelMesh;
    public void Steer(float steerInput)
    {
        wheelCollider.steerAngle = steerInput * maxAngle;
    }
    public void Accelerate(float powerInput)
    {
        wheelCollider.motorTorque = powerInput;
    }
    public void UpdateWheelPosition()
    {
        if (wheelMesh != null)
        {
            Vector3 pos;
            Quaternion rot;
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelMesh.rotation = rot;
            wheelMesh.position = pos;
        }
    }
    public void SetBreakTorque(float breakPower)
    {
        wheelCollider.brakeTorque = breakPower;
    }
    public float GetMotorTorque()
    {
        return wheelCollider.motorTorque;
    }
}
