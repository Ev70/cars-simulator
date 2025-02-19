using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] Material green, yellow, red;
    [SerializeField] int greenDuration, yellowDuration, redDuration = 1;

    IEnumerator Traffic()
    {
        while (true)
        {
            green.color = Color.green;
            red.color = Color.gray;

            yield return new WaitForSeconds(greenDuration);

            yellow.color = Color.yellow;
            green.color = Color.gray;

            yield return new WaitForSeconds(yellowDuration);

            red.color = Color.red;
            yellow.color = Color.gray;

            yield return new WaitForSeconds(redDuration);

        }
    }
    private void OnApplicationQuit()
    {
        green.color = Color.gray;
        yellow.color = Color.gray;
        red.color = Color.gray;
    }
    private void Start()
    {
        StartCoroutine(Traffic());
    }
}
