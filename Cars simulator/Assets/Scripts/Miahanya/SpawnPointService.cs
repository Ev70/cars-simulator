using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnPointService : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] spawnPoints;
    public int savedPoint;
    private void Start()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {

            spawnPoints[i].GetComponent<SpawnPoint>().SetValues(i);
        }
    }
}
