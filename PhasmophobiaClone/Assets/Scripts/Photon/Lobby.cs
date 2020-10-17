using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject mainPanel, ProgressLabel;

    string gameVersion = "1";

    private byte maxPlayerPerRoom = 4;

    bool isConnecting;
    void Start()
    {
        mainPanel.SetActive(true);
        ProgressLabel.SetActive(false);
    }
    public void Connect()
    {
        ProgressLabel.SetActive(true);
        mainPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            //PhotonNetwork.JoinRandomRoom();
            //Player connected, Can send him to lobby
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            print("Launcher: OnConnectedToMaster() was called by PUN");
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;
        ProgressLabel.SetActive(false);
        mainPanel.SetActive(true);
        Debug.LogWarningFormat("Launcher: onDisconnectedToMaster()" + cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No random room was created,So we create one room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayerPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom was called by PUN, Client is in a room");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("MainLevel");
        }
    }

}
