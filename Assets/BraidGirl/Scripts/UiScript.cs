using System;
using System.Collections;
using System.Collections.Generic;
using BraidGirl;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = BraidGirl.PlayerInput;

public class UiScript : MonoBehaviour
{
    private PlayerInput _playerInput;
    private bool _isPaused;
    private bool _isStarted;

    public GameObject pauseMenu;
    public GameObject titleAudioSourceObj;
    public GameObject birdsAudioSourceObj;
    public GameObject titleMenu;

    private void Awake()
    {
        // Initialize reference variables
        _playerInput = new PlayerInput();

        // Set the player input callbacks
        _playerInput.Character.PauseMenu.started += StartMenu;
        _playerInput.Character.StartGame.started += StartGame;
    }

    private void OnEnable()
    {
        _playerInput.Character.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Character.Disable();
    }

    private void StartMenu(InputAction.CallbackContext context)
    {
        if (_isPaused)
        {
            Time.timeScale = 1.0f;
            Debug.Log(Time.timeScale + " �������");
            pauseMenu.SetActive(false);
            _isPaused = false;

        }

        else
        {

            Time.timeScale = 0f;
            Debug.Log(Time.timeScale + " �����");
            _isPaused = true;
            pauseMenu.SetActive(true);
        }

    }

    private void StartGame(InputAction.CallbackContext context)
    {
        if (!_isStarted)
        {

            AudioSource titleAudioSource = titleAudioSourceObj.GetComponent<AudioSource>();
            AudioSource birdsAudioSource = birdsAudioSourceObj.GetComponent<AudioSource>();
            titleAudioSource.mute = true;
            birdsAudioSource.Play();
            Time.timeScale = 1.0f;
            titleMenu.SetActive(false);
            _isStarted = true;
        }
    }

    public void Exit()
    {
        Debug.Log("Exit");
    }

    public void StartNewGame()
    {
        Debug.Log("StartNewGame");
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }

    public void Resume()
    {
        Debug.Log("Resume");
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        _isPaused = false;
    }
}
