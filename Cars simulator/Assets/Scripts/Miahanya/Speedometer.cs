using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Speedometer : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    public float speed;
    [SerializeField] TMP_Text maxText;
    [SerializeField] Transform arrow;
    [SerializeField] bool reversed = true;
    private void Awake()
    {
        maxText.text = maxSpeed.ToString();
    }
    private void Update()
    {
        
        int dir = (reversed) ? -1 : 1;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        arrow.localRotation = Quaternion.Euler(arrow.localRotation.x, arrow.localRotation.y, 180*(speed/maxSpeed)*dir);
    }
}
