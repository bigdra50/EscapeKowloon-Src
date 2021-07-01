using System;
using System.Collections.Generic;
using System.IO;
using EscapeKowloon.Scripts.Managers;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UnityEngine;
using Zenject;

namespace EscapeKowloon.Scripts.Network
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public delegate void PropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged);

        public static event PropertiesChanged RoomPropsChanged;
        public static event Action OnRoomCreated;
        public static event Action OnExistingRoomJoined;
        public static event Action OnPlayersChanged;

        public static IObservable<Unit> OnJoinedRoomAsObservable() =>
            _onJoinedRoom ?? (_onJoinedRoom = new Subject<Unit>());

        private static Subject<Unit> _onJoinedRoom;
        private string room = "VRMeetup";
        private string gameVersion = "0.1";

        private static readonly RoomOptions RoomOptions = new RoomOptions
        {
            MaxPlayers = 2,
            IsOpen = true,
            IsVisible = true,
        };


        private bool _createdRoom = false;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            if (PhotonNetwork.IsConnected)
            {
                OnConnectedToMaster();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }

            Debug.Log("Connecting...");
        }

        #region CONNECTION

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("Connected to master!");
            Debug.Log("Joining room...");

            //PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.JoinRoom(room);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("Disconnected with reason {0}", cause);
        }


        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room!");

            if (_createdRoom)
            {
                NetworkManager.OnRoomCreated?.Invoke();
            }
            else
            {
                NetworkManager.OnExistingRoomJoined?.Invoke();
            }


            _onJoinedRoom?.OnNext(Unit.Default);
            CreatePlayer();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogWarning("Room join failed " + message);
            _createdRoom = true;
            Debug.Log("Creating room...");
            PhotonNetwork.CreateRoom(room, RoomOptions, TypedLobby.Default);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            Debug.Log("Got " + roomList.Count + " rooms.");
            foreach (RoomInfo room in roomList)
            {
                Debug.Log("Room: " + room.Name + ", " + room.PlayerCount);
            }
        }

        // TODO: 初期化の動作確認のために使ってるので, CreatePlayerなどを修正する
         private PlatformType _platformType = PlatformType.VR;


        public void CreatePlayer()
        {
            var player = PhotonNetwork.LocalPlayer;
            var playerList = PhotonNetwork.PlayerList;
            print($"PlayerNo: {player.ActorNumber.ToString()}");
            print($"PlayerId: {player.UserId}");
            print($"Player count: {playerList.Length.ToString()}");

            //if (_platformType == PlatformType.Desktop)
            //    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "Camera"), desktopPlayer.transform.position, desktopPlayer.transform.rotation).SetActive(true);
            /*else*/
            if (_platformType == PlatformType.VR)
            {
                var avatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "Avatar"), Vector3.zero,
                    Quaternion.identity);
            }
            else
            {
                GameObject.Find("Escaper").SetActive(false);
            }

            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Avatar"), Vector3.zero, Quaternion.identity, 0);
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CustomHandLeft"), Vector3.zero, Quaternion.identity, 0);
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CustomHandRight"), Vector3.zero, Quaternion.identity, 0);
        }

        #endregion

        #region ROOM_PROPS

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            print($"Enter player: {newPlayer.UserId}");
            OnPlayersChanged?.Invoke();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            OnPlayersChanged?.Invoke();
        }

        public static bool SetCustomPropertySafe(string key, object newValue, WebFlags webFlags = null)
        {
            Room room = PhotonNetwork.CurrentRoom;
            if (room == null || room.IsOffline)
            {
                return false;
            }

            ExitGames.Client.Photon.Hashtable props = room.CustomProperties;

            if (room.CustomProperties.ContainsKey(key))
            {
                props[key] = newValue;
            }
            else
            {
                props.Add(key, newValue);
            }

            //ExitGames.Client.Photon.Hashtable newProps = new ExitGames.Client.Photon.Hashtable(1) { { key, newValue } };
            //Hashtable oldProps = new Hashtable(1) { { key, room.CustomProperties[key] } };
            return room.LoadBalancingClient.OpSetCustomPropertiesOfRoom(props /*, oldProps, webFlags);*/);
        }

        public static object GetCurrentRoomCustomProperty(string key)
        {
            Room room = PhotonNetwork.CurrentRoom;
            if (room == null || room.IsOffline || !room.CustomProperties.ContainsKey(key))
            {
                return null;
            }
            else
            {
                return room.CustomProperties[key];
            }
        }

        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            RoomPropsChanged?.Invoke(propertiesThatChanged);
        }

        #endregion
    }
}