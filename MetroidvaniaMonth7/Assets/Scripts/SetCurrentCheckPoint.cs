﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCurrentCheckPoint : MonoBehaviour
{
    [SerializeField]
    CheckpointSO checkpoint;
    IInteractable interact;

    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<IInteractable>();
        interact.OnInteract += HandleInteraction;
    }

    private void HandleInteraction()
    {
        Debug.Log("Interacting");
        if (checkpoint != null)
        {
            checkpoint.checkpointLocation = transform.position;
        }
    }

}