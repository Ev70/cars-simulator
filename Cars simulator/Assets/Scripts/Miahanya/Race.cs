using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class Race : MonoBehaviour
{
    [SerializeField] Material green, yellow;
    [SerializeField] GameObject[] redLightObjects;
    public bool isStarted;
    public bool isFinished;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public bool carStopped;
    public float timeAfterStart;
    public int bestTime;
    [SerializeField] AudioClip raceMusic;
    [SerializeField] RaceInfoTablo raceTablo;
    [SerializeField] FadeOutIn fadeIn;
    IEnumerator StartRace()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = raceMusic;
        audio.loop = true;
        audio.Play();

        raceTablo.StartTablo();

        foreach (GameObject obj in redLightObjects)
        {
            obj.GetComponent<Renderer>().material.color = Color.red;
        }
        yellow.color = Color.yellow;
        yield return new WaitWhile(() => carStopped == false);
        yellow.color = Color.gray;
        for (int i = 1; i <= redLightObjects.Length-1; i++)
        {
            redLightObjects[i-1].GetComponent<Renderer>().material.color = Color.gray;
            redLightObjects[i].GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.5f);
        }

        redLightObjects[redLightObjects.Length - 1].GetComponent<Renderer>().material.color = Color.gray;
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        green.color = Color.green;
        while (!isFinished)
        {
            timeAfterStart += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(audio);
        yield return new WaitForSeconds(10f);
        fadeIn.FadeAndGoToScene();
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
        bestTime = PlayerPrefs.GetInt("bestTime");
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
    public void CheckAndSaveBestTime(int sec, int min)
    {
        int time = min * 60 + sec;
        if (bestTime == 0 || time < bestTime)
        {
            PlayerPrefs.SetInt("bestTime", time);
            bestTime = PlayerPrefs.GetInt("bestTime");
        }
    }
}
