﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewLevelOnClick : MonoBehaviour
{
    [SerializeField]
    string levelName = "";
    [SerializeField]
    float loadTime = 1f;

    public void OnLevelClick()
    {
        Time.timeScale = 1;
        FMODUnity.RuntimeManager.PlayOneShot("event:/UIBUtton");
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}