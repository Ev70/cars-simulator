using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] Race race;
    [SerializeField] Transform L, R;
    [SerializeField] bool activate;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car") && race.rb.velocity == Vector3.zero && !activate)
        {
            activate = true;
            Debug.Log(1);
            race.carStopped = true;
            //StartCoroutine(rotateFences());
            L.Rotate(0, -90, 0);
            R.Rotate(0, 90, 0);
        }
    }
    IEnumerator rotateFences()
    {
        float t = 0;
        while (t <= 90)
        {
            t += Time.deltaTime;
            L.Rotate(0, -t*Time.deltaTime, 0);
            R.Rotate(0, t*Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
