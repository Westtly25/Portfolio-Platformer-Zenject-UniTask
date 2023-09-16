﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheatController : MonoBehaviour
{
    [SerializeField] 
    private float inputTimeToLive;
    [SerializeField]
    private CheatItem[] cheats;

    private string _currentInput;
    private float _inputTime;

    private void Awake()
    {
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char inputChar)
    {
        _currentInput += inputChar;
        _inputTime = inputTimeToLive;
        FindAnyCheats();
    }

    private void FindAnyCheats()
    {
        foreach (var cheatItem in cheats)
        {
            if (_currentInput.Contains(cheatItem.Name))
            {
                cheatItem.Action.Invoke();
                _currentInput = string.Empty;
            }
        }
    }

    private void Update()
    {
        if (_inputTime < 0)
        {
            _currentInput = string.Empty;
        }
        else
        {
            _inputTime -= Time.deltaTime;
        }
    }
    
    [Serializable]
    public class CheatItem
    {
        public string Name;
        public UnityEvent Action;
    }
}

