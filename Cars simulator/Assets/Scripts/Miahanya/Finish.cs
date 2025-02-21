using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] Race race;
    [SerializeField] SpawnPointService SPS;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && SPS.savedPoint == SPS.getLenght()-1)
        {
            race.isFinished = true;
        }
    }
}
