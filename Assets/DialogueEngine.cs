using System;
using System.Collections;
using System.Collections.Generic;using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class DialogueEngine : MonoBehaviour
{
    public static DialogueEngine instance;
    
    public Dialogue[] dialogue;
    public TextReader textReader;
    
    private int index = 0;

    private bool canProceed = false;
    
    private void Awake()
    {
        instance = this;
        textReader = GetComponent<TextReader>();
    }

    private void Start()
    {
        ShowNext();
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && canProceed)
        {
            if (index <= dialogue.Length-1) ShowNext();
        }
        else
        {
            
        }
    }

    public void ShowNext()
    {
        canProceed = false;
        textReader.ShowText(dialogue[index].name, dialogue[index].text, () => { dialogue[index].onDialogueStart?.Invoke(); }, () =>
        {
            dialogue[index].onDialogueFinish?.Invoke();
            
            // Check for dialogue
            if (dialogue[index].dialogueChoices.Length != 0)
            {
                ChoiceSpawner.instance.AskChoice(dialogue[index].dialogueChoices);
                
                return;
            }
            
            if (dialogue[index].jumpIndex) index = dialogue[index].index;
            canProceed = true;
            
            index++;
        });
    }

    public void JumpTo(int newIndex)
    {
        index = newIndex;
        ShowNext();
    }
}
    
[System.Serializable]
public struct Dialogue
{
    public string name;
    
    [TextArea]
    public string text;

    public DialogueChoice[] dialogueChoices;

    public UnityEvent onDialogueStart, onDialogueFinish;
    
    public bool jumpIndex;

    public int index;
}

[System.Serializable]
public struct DialogueChoice
{
    public string choiceText;

    [FormerlySerializedAs("onDialogue")] public UnityEvent onChoice;
}