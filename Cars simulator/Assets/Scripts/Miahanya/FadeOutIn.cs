using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutIn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Color colorFade;
    [SerializeField] fadeState state;
    [SerializeField] float durationFade = 1f;
    [SerializeField] Image fade;

    public void Fade()
    {
        StartCoroutine(FadeCor());
    }
    IEnumerator FadeCor()
    {
        int target;
        target = (state == fadeState.Out) ? 1 : 0;
        float t = target;
        while (t >= 0 && target == 1 || t <= 1 && target == 0)
        {
            int dir = (target == 0) ? 1 : -1;
            t += Time.deltaTime * durationFade  * dir;
            fade.color = new(colorFade.r, colorFade.g, colorFade.b, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
public enum fadeState {
    Out, In // Out из цвета в прозрачный / In из прозрачного в цвет
}