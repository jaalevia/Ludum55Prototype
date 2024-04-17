using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMusic : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenuTheme");

    }
}
