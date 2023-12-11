using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    void Start()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
