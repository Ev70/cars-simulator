using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadScene : MonoBehaviour
{
    [SerializeField] FadeOutIn fade;
    public void ReloadAndFadeScene()
    {
        fade.FadeAndGoToScene(SceneManager.GetActiveScene().buildIndex);
    }
}
