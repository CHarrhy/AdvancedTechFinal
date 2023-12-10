using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            source.PlayOneShot(clip);
            Debug.Log("Sound should play");
        }
    }
}
