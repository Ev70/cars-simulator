using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
public class Speedometer : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float speed;
    [SerializeField] TMP_Text maxText;
    [SerializeField] Transform arrow;
    [SerializeField] bool reversed = true;
    private float lastspeed;
    private void Awake()
    {
        maxText.text = maxSpeed.ToString();
    }
    private void OnValidate()
    {
        if (speed != lastspeed)
        {
            Debug.Log("Changed");
            lastspeed = speed;
            int dir = (reversed) ? -1 : 1;
            arrow.rotation = Quaternion.Euler(arrow.rotation.x, arrow.rotation.y, 180*(speed/maxSpeed)*dir);
        }
    }
}
