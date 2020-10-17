using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomButton : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text sizeText;

    private string roomName;
    private int playerCount;
    
    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    internal void SetRoom(string nameInput, int sizeInput, int CountInput)
    {
        sizeInput = 5;

        roomName = nameInput;
        playerCount = CountInput;
        nameText.text = nameInput;
        sizeText.text = CountInput + "/" + sizeInput;
    }
}
