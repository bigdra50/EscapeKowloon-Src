using System.Collections;
using System.Collections.Generic;
using EscapeKowloon.Scripts.Managers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class SceneParameterBindTest : MonoBehaviour
{
    [Inject] private PlatformType _platform;

    // Start is called before the first frame update
    void Start()
    {
        //print($"{_platform}");
    }

    // Update is called once per frame
    void Update()
    {
        var allPlayers = PhotonNetwork.PlayerList;
        //print($"{allPlayers.Length} Players");
        foreach (var player in allPlayers)
        {
            //print($"Player Id: {player.UserId}");
            //print($"Player Name: {player.NickName}");
        }
    }
}