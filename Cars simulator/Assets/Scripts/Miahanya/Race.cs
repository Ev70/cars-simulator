using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] Material green, yellow;
    [SerializeField] GameObject[] redLightObjects;
    private bool isStarted;
    private bool timerStopped;
    [SerializeField] bool carStopped = true;
    public float timeAfterStart;
    [SerializeField] AudioClip raceMusic;
    IEnumerator StartRace()
    {
        AudioSource audio = new();
        audio.transform.SetParent(transform);
        audio.clip = raceMusic;
        audio.loop = true;
        audio.Play();
        foreach(GameObject obj in redLightObjects)
        {
            obj.GetComponent<Renderer>().material.color = Color.red;
        }
        yellow.color = Color.yellow;
        yield return new WaitWhile(() => !carStopped); // Здесь должна быть полная остановка я пока поставил бул
        yellow.color = Color.gray;
        GameObject lastObj = redLightObjects[0];
        foreach (GameObject obj in redLightObjects)
        {
            if (lastObj != obj)
            {
                lastObj.GetComponent<Renderer>().material.color = Color.gray;
                obj.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                lastObj.GetComponent<Renderer>().material.color = Color.red;
            }
            yield return new WaitForSeconds(2f);
        }

        redLightObjects[redLightObjects.Length-1].GetComponent<Renderer>().material.color = Color.gray;
        yield return new WaitForSeconds(Random.RandomRange(0.5f, 2f));
        green.color = Color.green;
        //Должен запуститься таймер
        while (!timerStopped)
        {
            timeAfterStart += Time.deltaTime;
        }
    }
    private void OnApplicationQuit()
    {
        green.color = Color.gray;
        yellow.color = Color.gray;
        foreach (GameObject obj in redLightObjects)
        {
            obj.GetComponent<Renderer>().material.color = Color.gray;
        }
    }
    private void Start()
    {

        green.color = Color.gray;
        yellow.color = Color.gray;
        foreach (GameObject obj in redLightObjects)
        {
            obj.GetComponent<Renderer>().material.color = Color.red;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Car") && !isStarted)
        {
            isStarted = true;
            StartCoroutine(StartRace());
        }
        
    }
}
