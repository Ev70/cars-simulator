using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;
using Unity.VisualScripting;
public class FadeOutIn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Color colorFade;
    [SerializeField] fadeState state;
    [SerializeField] float durationFade = 1f;
    [SerializeField] Light DirectLight;
    [SerializeField] int index;
    [SerializeField] VisualEffect visualEffect;
    public void FadeAndGoToScene()
    {
        StartCoroutine(FadeCor(index));
    }
    public void FadeAndGoToScene(int SceneIndex)
    {
        StartCoroutine(FadeCor(SceneIndex));
    }
    private void OnApplicationQuit()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 1);
        DirectLight.intensity = 1;
    }
    IEnumerator FadeCor(int SceneIndex)
    {
        float t = (state == fadeState.In) ? 1 : 0;
        if (state == fadeState.Out && visualEffect != null)
        {
            yield return new WaitForSeconds(2f);
            visualEffect.Play();
        }
        while (t >= 0 && state == fadeState.In || t <= 1 && state == fadeState.Out)
        {
            int dir = (state == fadeState.In) ? -1 : 1;
            t += Time.deltaTime / dir * durationFade;
            RenderSettings.skybox.SetFloat("_Exposure", t);
            RenderSettings.sun.intensity = t;
            DirectLight.intensity = t;
            yield return new WaitForEndOfFrame();
        }
        if (state == fadeState.In)
        {
            SceneManager.LoadScene(SceneIndex);
        }
        
    }

}
public enum fadeState {
    Out, In // Out из темноты в свет / In из света в темноту
}