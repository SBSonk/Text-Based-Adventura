using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSpawner : MonoBehaviour
{
    public static ChoiceSpawner instance;

    public Transform buttonParent;
    public Button buttonPrefab;
    public float ySpacing = 150;

    public GameObject background;

    private void Awake()
    {
        instance = this;
    }

    public void AskChoice(DialogueChoice[] dialogueChoices)
    {
        DestroyChildren();
        background.SetActive(true);
        
        for (int i = 0; i < dialogueChoices.Length; i++)
        {
            var newBtn = Instantiate(buttonPrefab, buttonParent);
            newBtn.GetComponent<RectTransform>().position = buttonParent.position + new Vector3(0, -i * ySpacing);
            
            newBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(dialogueChoices[i].choiceText);

            var action = dialogueChoices[i].onChoice;

            newBtn.onClick.AddListener(() =>
            {
                action?.Invoke();
                DestroyChildren();
                
                background.SetActive(false);
            });
        }
    }

    void DestroyChildren()
    {
        // delete all children
        foreach (Transform child in buttonParent) {
            Destroy(child.gameObject);
        }
    }
}
