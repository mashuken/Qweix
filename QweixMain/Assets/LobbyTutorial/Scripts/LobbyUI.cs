using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour {


    public static LobbyUI Instance { get; private set; }


    [SerializeField] private Transform playerSingleTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private TextMeshProUGUI gameModeText;
    [SerializeField] private Button changeMarineButton;
    [SerializeField] private Button changeNinjaButton;
    [SerializeField] private Button changeZombieButton;
    [SerializeField] private Button leaveLobbyButton;
    [SerializeField] private Button changeGameModeButton;


    private void Awake() {
        Instance = this;

        playerSingleTemplate.gameObject.SetActive(false);

        changeMarineButton.onClick.AddListener(() => {
            LobbyManagerTutorial.Instance.UpdatePlayerCharacter(LobbyManagerTutorial.PlayerCharacter.Marine);
        });
        changeNinjaButton.onClick.AddListener(() => {
            LobbyManagerTutorial.Instance.UpdatePlayerCharacter(LobbyManagerTutorial.PlayerCharacter.Ninja);
        });
        changeZombieButton.onClick.AddListener(() => {
            LobbyManagerTutorial.Instance.UpdatePlayerCharacter(LobbyManagerTutorial.PlayerCharacter.Zombie);
        });

        leaveLobbyButton.onClick.AddListener(() => {
            LobbyManagerTutorial.Instance.LeaveLobby();
        });

        changeGameModeButton.onClick.AddListener(() => {
            LobbyManagerTutorial.Instance.ChangeGameMode();
        });
    }

    private void Start() {
        LobbyManagerTutorial.Instance.OnJoinedLobby += UpdateLobby_Event;
        LobbyManagerTutorial.Instance.OnJoinedLobbyUpdate += UpdateLobby_Event;
        LobbyManagerTutorial.Instance.OnLobbyGameModeChanged += UpdateLobby_Event;
        LobbyManagerTutorial.Instance.OnLeftLobby += LobbyManager_OnLeftLobby;
        LobbyManagerTutorial.Instance.OnKickedFromLobby += LobbyManager_OnLeftLobby;

        Hide();
    }

    private void LobbyManager_OnLeftLobby(object sender, System.EventArgs e) {
        ClearLobby();
        Hide();
    }

    private void UpdateLobby_Event(object sender, LobbyManagerTutorial.LobbyEventArgs e) {
        UpdateLobby();
    }

    private void UpdateLobby() {
        UpdateLobby(LobbyManagerTutorial.Instance.GetJoinedLobby());
    }

    private void UpdateLobby(Lobby lobby) {
        ClearLobby();

        foreach (Player player in lobby.Players) {
            Transform playerSingleTransform = Instantiate(playerSingleTemplate, container);
            playerSingleTransform.gameObject.SetActive(true);
            LobbyPlayerSingleUI lobbyPlayerSingleUI = playerSingleTransform.GetComponent<LobbyPlayerSingleUI>();

            lobbyPlayerSingleUI.SetKickPlayerButtonVisible(
                LobbyManagerTutorial.Instance.IsLobbyHost() &&
                player.Id != AuthenticationService.Instance.PlayerId // Don't allow kick self
            );

            lobbyPlayerSingleUI.UpdatePlayer(player);
        }

        changeGameModeButton.gameObject.SetActive(LobbyManagerTutorial.Instance.IsLobbyHost());

        lobbyNameText.text = lobby.Name;
        playerCountText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        gameModeText.text = lobby.Data[LobbyManagerTutorial.KEY_GAME_MODE].Value;

        Show();
    }

    private void ClearLobby() {
        foreach (Transform child in container) {
            if (child == playerSingleTemplate) continue;
            Destroy(child.gameObject);
        }
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

}