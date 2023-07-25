using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject game;

    public void Play()
    {
        game.SetActive(true);
        _camera.transform.DOMoveY(124f, 5);
        startCanvas.SetActive(false);
    }
    public void Quit(){
        Application.Quit();
    }
}
