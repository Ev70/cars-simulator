using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;
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
        StartCoroutine(FadeCor());
    }
    IEnumerator FadeCor()
    {
        int target;
        if (state == fadeState.In)
        {
            target = 0;
        }
        else
        {
            target = 1;
        }
        float t = (state == fadeState.In) ? 1 : 0;
        while (t >= 0 && state == fadeState.In || t <= 1 && state == fadeState.Out)
        {
            Debug.Log(t);
            int dir = (state == fadeState.In) ? -1 : 1;
            t += Time.deltaTime / dir * durationFade;
            RenderSettings.skybox.SetFloat("_Exposure", t);
            DirectLight.intensity = t;
            yield return new WaitForEndOfFrame();
        }
    }
}
public enum fadeState {
    Out, In // Out из темноты в свет / In из света в темноту
}