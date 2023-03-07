using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] lobbyWindows;
    [SerializeField] private GameObject background;
    [SerializeField] private int startWindow;

    //CreateLobbySettings
    [SerializeField] private GameObject LobbyManager;

    private void Start()
    {
        LoadWindow(lobbyWindows[startWindow]);
    }

    public void LoadWindow(GameObject window)
    {
        ClearWindows();
        window.SetActive(true);
    }

    private void ClearWindows()
    {
        if (lobbyWindows != null)
        {
            foreach (GameObject window in lobbyWindows)
            {
                window.SetActive(false);
            }
        }
    }

    public void UpdatePlayerNumber(TMP_Text playerNumber)
    {
        if (int.Parse(playerNumber.text) == 2)
        {
            playerNumber.text = "4";
        }
        if (int.Parse(playerNumber.text) == 4)
        {
            playerNumber.text = "6";
        }
        if (int.Parse(playerNumber.text) == 6)
        {
            playerNumber.text = "2";
        }
    }
}
