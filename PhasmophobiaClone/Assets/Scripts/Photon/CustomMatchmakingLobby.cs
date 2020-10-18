using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomMatchmakingLobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject lobbyConnectButton, lobbyPanel,mainPanel;
    [SerializeField]
    private InputField PlayerNameIF;

    private string roomName;

    private List<RoomInfo> roomListings;
    [SerializeField]
    private Transform roomContainer;
    [SerializeField]
    private GameObject roomListingPrefab;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        lobbyConnectButton.SetActive(true);
        roomListings = new List<RoomInfo>();

        #region PlayerNickName
        //if (PlayerPrefs.HasKey("NickName"))
        //{
        //    if (PlayerPrefs.GetString("NickName") == "")
        //    {

        //    }
        //}
        #endregion

        PlayerNameIF.text = PhotonNetwork.NickName;

    }

    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public void JoinLobbyOnClick()
    {
        mainPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if(roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }

            if(tempIndex != -1)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomContainer.GetChild(tempIndex).gameObject);
            }

            if(room.PlayerCount > 0)
            {
                roomListings.Add(room);
                ListRoom(room);
            }

        }
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void OnRoomSizeChanegd(string sizeIn)
    {
        //room size change.
    }

    public void CreateRoom()
    {
        RoomOptions roomOps = new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = (byte)5 };
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
        //Display error msg to player
    }
    public void MatchmakingCancel()
    {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
}