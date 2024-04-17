using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;


    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    [Header("Dialogue UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueTextDisplay;
    [SerializeField] TextMeshProUGUI _displayNameText;
    [SerializeField] private Animator _portraitAnimator;
    private Animator _layoutAnimator;
    [SerializeField] private float _typingSpeed = 0.04f;

    private bool _canSkip;
    private bool _submitSkip;
    private Coroutine _displayLineCoroutine;

    private bool _canContinueToNextLine = false;

    private Story _currentStory;
    public bool DialogueIsActive { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Dialogue Manager");
        }
        instance = this;
    }

    private void Start()
    {
        DialogueIsActive = false;
        _dialoguePanel.SetActive(false);

        _layoutAnimator = _dialoguePanel.GetComponent<Animator>();
    }

    public static DialogueManager GetInstance()
    { 
        return instance;
    }

    public void EnterDialogue(TextAsset inkJSON)
    {
        _currentStory = new Story(inkJSON.text);
        DialogueIsActive = true;
        _dialoguePanel.SetActive(true);
        ContinueStory();

        _displayNameText.text = "???";
        _portraitAnimator.Play("default");
        _layoutAnimator.Play("left");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _submitSkip = true;
        }
        if (!DialogueIsActive)
        { return; }
        
        if (_canContinueToNextLine && Input.GetMouseButtonDown(0))
            {
            ContinueStory(); }
    }

    public void ExitDialogue()
    { 
        DialogueIsActive = false;
        _dialoguePanel.SetActive(false);
        _dialogueTextDisplay.text = "";
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            if (_displayLineCoroutine != null)
            {
                StopCoroutine(_displayLineCoroutine);
            }
            _displayLineCoroutine = StartCoroutine(DisplayLine(_currentStory.Continue()));

            HandleTags(_currentStory.currentTags);
        }
        else
        {
            ExitDialogue();
        }
    }

    private IEnumerator CanSkip()
    {
        _canSkip = false;
        yield return null;
        _canSkip = true;
    }

    private IEnumerator DisplayLine(string line)
    {
        _dialogueTextDisplay.text = "";

        _submitSkip = false;

        _canContinueToNextLine = false;

        StartCoroutine(CanSkip());
        foreach (char letter in line.ToCharArray())
        {
            if (_canSkip && _submitSkip)
            {
                _submitSkip = false;
                _dialogueTextDisplay.text = line;
                break;
            }

            _dialogueTextDisplay.text += letter;
            FindObjectOfType<AudioManager>().Play("CalmCat");
            yield return new WaitForSeconds(_typingSpeed);
        }
        _canContinueToNextLine = true;
        _canSkip = false;
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag can't be parsed:" + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    _displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    _portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    _layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Somethigs wrong:" + tag);
                    break;
            }
        }
    }
}
