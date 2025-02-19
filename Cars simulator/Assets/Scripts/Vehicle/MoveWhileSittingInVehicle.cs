using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveWhileSittingInVehicle : MonoBehaviour
{
    [SerializeField] bool isMoving = true;
    [SerializeField] InputActionProperty isMovingToggle;
    [SerializeField] InputActionProperty moveWhileSittingHorizontal;
    [SerializeField] InputActionProperty moveWhileSittingVertical;
    [SerializeField] float moveSpeed = 1;

    [SerializeField] Transform borders;

    [SerializeField] Vehicle vehicle;

    void Update()
    {
        //Debug.Log(isMovingToggle.action.ReadValue<bool>());
        
        if (isMovingToggle.action.ReadValue<bool>())
        {
            ToggleMoving();
        }
        if (isMoving && vehicle.isPlayerInVehicle)
        {
            Vector2 horizontal = moveWhileSittingHorizontal.action.ReadValue<Vector2>();
            Vector2 vertical = moveWhileSittingVertical.action.ReadValue<Vector2>();
            vehicle.player.localPosition += new Vector3(horizontal.x, vertical.y, horizontal.y) * moveSpeed * Time.deltaTime;
            vehicle.player.position = new Vector3(Mathf.Clamp(vehicle.player.position.x, borders.position.x - borders.localScale.x/2, borders.position.x + borders.localScale.x / 2), 
                Mathf.Clamp(vehicle.player.position.y, borders.position.y - borders.localScale.y / 2, borders.position.y + borders.localScale.y / 2), 
                Mathf.Clamp(vehicle.player.position.z, borders.position.z - borders.localScale.z / 2, borders.position.z + borders.localScale.z / 2));
        }
    }
    public void ToggleMoving()
    {
        isMoving = !isMoving;
    }
}
