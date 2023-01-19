using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{

    //Adds the UI buttons as variables.
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button shutdownButton;
    [SerializeField] private Button disconnectButton;
    [SerializeField] private Button connectButton;
    [SerializeField] private TMP_InputField ipAddressInput;
    [SerializeField] private Button backButton;

    //This is to set the state of the UI
    enum UiState
    {
        SelectionBtns,
        InputIp,
        DisconnectBtn,
        ShutdownBtn
    }

    //SelectionBtns: Shows the Client, Host or Server selection buttons.
    //InputIp: Shows the interface to input an IP to connect to.
    //DisconnectBtn: Shows the Button to disconnect the client. Only available as Client.
    //ShutdownBtn: Shows the Button to shutdown the server. Only available as Host or Server.

    private void Awake()
    {
        UpdateNetUI(UiState.SelectionBtns);

        //These are all Listeners to run code when a button on the UI is pressed
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            UpdateNetUI(UiState.ShutdownBtn);
        });
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            UpdateNetUI(UiState.ShutdownBtn);
        });
        clientButton.onClick.AddListener(() =>
        {
            //NetworkManager.Singleton.StartClient();
            UpdateNetUI(UiState.InputIp);
        });
        shutdownButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            UpdateNetUI(UiState.SelectionBtns);
        });
        disconnectButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.DisconnectClient(NetworkManager.Singleton.LocalClientId);
            UpdateNetUI(UiState.SelectionBtns);
        });
        connectButton.onClick.AddListener(() =>
        {
            //fetch someString from ipAddressInput.text
            //using this currently https://forum.unity.com/threads/how-do-i-change-unity-transport-ip-address-and-port-during-runtime.1314591/
            if(ipAddressInput.text == null)
            {
                return;
            }
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipAddressInput.text;
            NetworkManager.Singleton.StartClient();
            UpdateNetUI(UiState.DisconnectBtn);
        });
        backButton.onClick.AddListener(() =>
        {
            UpdateNetUI(UiState.SelectionBtns);
        });
    }

    //Disables all UI elements. Used to make a clean slate before enabling the current state's UI.
    private void ClearNetUI()
    {
        serverButton.gameObject.SetActive(false);
        hostButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
        disconnectButton.gameObject.SetActive(false);
        shutdownButton.gameObject.SetActive(false);
        connectButton.gameObject.SetActive(false);
        ipAddressInput.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);

    }

    //Called when pressing a button. Clears the UI and then adds the appropriate buttons based on the state.
    private void UpdateNetUI(UiState uiCurrentState)
    {
        if (uiCurrentState == UiState.SelectionBtns)
        {
            ClearNetUI();
            serverButton.gameObject.SetActive(true);
            hostButton.gameObject.SetActive(true);
            clientButton.gameObject.SetActive(true);
        }
        if (uiCurrentState == UiState.InputIp)
        {
            ClearNetUI();
            connectButton.gameObject.SetActive(true);
            ipAddressInput.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);
        }
        if (uiCurrentState == UiState.DisconnectBtn)
        {
            ClearNetUI();
            disconnectButton.gameObject.SetActive(true);
        }
        if (uiCurrentState == UiState.ShutdownBtn)
        {
            ClearNetUI();
            shutdownButton.gameObject.SetActive(true);
        }
    }
}
