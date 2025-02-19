using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int order;
    public SpawnPointService SPS;
    public void SetValues(int PointOrder)
    {
        order = PointOrder; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && order - SPS.savedPoint == 1)
        {
            SPS.savedPoint = order;
        }
    }
}
