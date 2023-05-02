using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Constants : MonoBehaviour
{
    public List<PlayerInput> players = new List<PlayerInput>();

    public static Constants Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("trying to create another instance");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
    }
}
