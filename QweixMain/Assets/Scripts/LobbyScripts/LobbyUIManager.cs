using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void UpdatePlayerNumber()
    {

    }
}
