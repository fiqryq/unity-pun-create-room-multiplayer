using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputFiend;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;

    void Start()
    {
        Debug.Log("Connecting To Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("On Joined Lobby");
        MenuManager.Instance.OpenMenu("title");
    }

    public void CreateRoom() {
        if (string.IsNullOrEmpty(roomNameInputFiend.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputFiend.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        errorText.text = "Room Creation Failed: " + message;
        Debug.LogError("Room Creation Failed: " + message);
        MenuManager.Instance.OpenMenu("error");
    }
}
