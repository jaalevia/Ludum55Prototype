using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public Animator Animator;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenuTheme");
    }
    
    public void Fade()
    {
        Animator.SetTrigger("FadeOut");
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
