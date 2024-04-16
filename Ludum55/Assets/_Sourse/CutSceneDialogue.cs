using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneDialogue : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    public void PlayCutsceneDialogue()
    {
        DialogueManager.GetInstance().EnterDialogue(inkJSON);
    }
}
