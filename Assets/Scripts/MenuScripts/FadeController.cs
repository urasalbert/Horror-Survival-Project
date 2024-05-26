using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private void Start()
    {
        FadeInOutScene.Instance.FadeOut();
    }
}
