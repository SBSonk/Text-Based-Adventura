using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextReader : MonoBehaviour
{
    public static TextReader main;
    
    public bool displayingText = false;
    public float characterTime = 0.1f;
    public float specialCharacterTime = 0.5f;
    public float sentenceTimePadding = 0.5f;
    public TextMeshProUGUI textReaderUI, nameUI;
    public GameObject continueArrow;

    string lastText;
    UnityAction OnTextEnd;

    public void ShowText(string name, string text, UnityAction OnTextStart, UnityAction OnTextFinish)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayTextAnimation(name, text, OnTextStart, OnTextFinish));
    }

    private void Awake()
    {
        main = this;
    }
    
    public void SkipAnimationText()
    {
        StopAllCoroutines();

        textReaderUI.SetText(lastText);
        OnTextEnd?.Invoke();

        continueArrow.SetActive(true);

        displayingText = false;

        OnTextEnd = null;
    }
    
    IEnumerator DisplayTextAnimation(string name, string text, UnityAction OnTextStart, UnityAction OnTextFinish)
    {
        OnTextStart?.Invoke();
        displayingText = true;

        OnTextEnd = OnTextFinish;   
        
        continueArrow.SetActive(false);

        nameUI.SetText(name);
        
        string shownText = "";

        WaitForSeconds charTimer = new WaitForSeconds(characterTime);
        WaitForSeconds commaTimer = new WaitForSeconds(specialCharacterTime);

        lastText = text;

        for (int i = 0; i < text.Length; i++)
        {
            shownText += text[i];
            textReaderUI.text = shownText;

            // todo: add extra spacing for expressions (, ! "word" .) close enough
            if (text[i] == ',')
            {
                yield return commaTimer;
            }
            else yield return charTimer;
        }

        textReaderUI.text = shownText;

        yield return new WaitForSeconds(sentenceTimePadding);
        
        OnTextFinish?.Invoke();

        continueArrow.SetActive(true);
        
        displayingText = false;
    }

    public void ShowLoadingText() => textReaderUI.text = "...";
}
