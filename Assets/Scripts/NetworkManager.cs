using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

/*    // Update is called once per frame
    void Update()
    {
        
    }*/

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Trying to connect to server.....");
        //print when trying to connect to the server
    }

    /*overriding the OnConnectedToMaster function to add other functionalities, e.g. knowing when we joined a room with the debug
     */
    public override void OnConnectedToMaster()
    {
        //print when connected to the server
        Debug.Log("....Connected to server");
        base.OnConnectedToMaster();//running the original function
        //Joining a room to share data between connected players
        RoomOptions roomOption = new RoomOptions();//creating room option
        roomOption.MaxPlayers = 3;//setting max number of players
        roomOption.IsVisible = true;//making the room option visible for players
        roomOption.IsOpen = true;//make it so we can join the room after it is created

        PhotonNetwork.JoinOrCreateRoom("Room1", roomOption, TypedLobby.Default);
    }

    //override the OnJoinedRoom function so we can know in the debug when we joined a room
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        base.OnJoinedRoom();//running the original function
    }

    //override the OnPlayerEnteredRoom function so we can know in the debug when a new player joined a room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
