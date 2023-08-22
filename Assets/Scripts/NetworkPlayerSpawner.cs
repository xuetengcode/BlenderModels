using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;


    //override the function called when we join a room
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        //instantiating player prefab
        //NOTE
        //ensure the prefab name is the same as the name of the prefab you want to use
        //ensure the prefab you want to use is in the folder named "Resources"
        //ensure the prefab has the "PhotonView" component attached to it
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("NetworkPlayer", transform.position, transform.rotation);//instantiate the spawned player when we join a room
    }

    //override the function called when we leave a room
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);//destroy the spawned player when we join a room
    }
}
