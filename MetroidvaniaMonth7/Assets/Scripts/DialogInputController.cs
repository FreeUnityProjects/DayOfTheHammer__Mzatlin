﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DialogInputController : MonoBehaviour,IDialogInput
{
    public event Action OnDialogInput = delegate { };
    [SerializeField]
    TextMeshProUGUI continueText;
    IActiveDialog active;
    IWriteDialog dialog;
    ITypeCharacter type;

    // Start is called before the first frame update
    void Start()
    {
        active = GetComponent<IActiveDialog>();
        dialog = GetComponent<IWriteDialog>();
        type = GetComponent<ITypeCharacter>();
    }
    

    void OnDialogue()
    {
        if(active.IsActive && continueText.enabled)
        {
            CheckDialogLine();
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        CheckContinueText();
    }

    void CheckContinueText()
    {
        if (dialog.DialogLines[type.TypingIndex] == type.TypeContent.text)
        {
            continueText.enabled = true;
        }
        else
        {
            continueText.enabled = false;
        }
    }

    void CheckDialogLine()
    {
        if (continueText.enabled)
        {
            OnDialogInput();
        }
    }
}
