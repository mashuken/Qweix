using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyAssets : MonoBehaviour {



    public static LobbyAssets Instance { get; private set; }


    [SerializeField] private Sprite marineSprite;
    [SerializeField] private Sprite ninjaSprite;
    [SerializeField] private Sprite zombieSprite;


    private void Awake() {
        Instance = this;
    }

    public Sprite GetSprite(LobbyManagerTutorial.PlayerCharacter playerCharacter) {
        switch (playerCharacter) {
            default:
            case LobbyManagerTutorial.PlayerCharacter.Marine:   return marineSprite;
            case LobbyManagerTutorial.PlayerCharacter.Ninja:    return ninjaSprite;
            case LobbyManagerTutorial.PlayerCharacter.Zombie:   return zombieSprite;
        }
    }

}