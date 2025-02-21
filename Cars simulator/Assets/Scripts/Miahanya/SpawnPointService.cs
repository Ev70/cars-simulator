using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnPointService : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] spawnPoints;
    public int savedPoint;
    private void Start()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {

            spawnPoints[i].GetComponent<SpawnPoint>().SetValues(i);
        }
    }
    private bool CheckPoint(int Current)
    {
        if (Current-savedPoint == 1)
        {
            savedPoint = Current;
            return true;
        }
        return false;
    }
    public int getLenght()
    {
        return spawnPoints.Length;
    }
}
