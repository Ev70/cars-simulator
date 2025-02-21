using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FadeOnStart : MonoBehaviour
{
    [SerializeField] FadeOutIn fade;
    private void Awake()
    {
        fade.FadeAndGoToScene();
    }
}
