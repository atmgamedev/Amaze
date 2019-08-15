using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class connexion : MonoBehaviour {

	/// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
	public bool AutoConnect = true;
	public bool joinlobby = true;
	public byte Version = 1;

	/// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
	private bool ConnectInUpdate = true;


	public virtual void Start()
	{
		PhotonNetwork.autoJoinLobby = joinlobby;    // we join randomly. always. no need to join a lobby to get the list of rooms.
        PhotonNetwork.automaticallySyncScene = true;
	}

	public virtual void Update()
	{
		if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
		{
			Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

			ConnectInUpdate = false;
			PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
		}
	}

	public virtual void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
		PhotonNetwork.JoinRandomRoom();
	}

	public virtual void OnJoinedLobby()
	{
		GlobalUI.instance.ShowTablePanel ();
		Debug.Log("Connecté au lobby");
		GetRoomList.instance.createroom.interactable = true;
		GetComponent<ShowStatus> ().enabled = false;
	}

	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.LogError("Cause: " + cause);
	}

	public void OnJoinedRoom()
	{
		if(GetRoomList.instance.RoomEmpty.activeSelf){
			GetRoomList.instance.RoomEmpty.SetActive (false);
		}
		GlobalUI.instance.ShowRoomPanel ();
		ManageListMembers.instance.launch ();
	}

	public void OnLeftRoom(){
		PhotonNetwork.JoinLobby();
		GlobalUI.instance.HideAllPanel ();
	}

	public void ConnexionLogin(string pseudo){
		GetComponent<ShowStatus> ().enabled = true;
		globalVariables.pseudo = pseudo;
		PhotonNetwork.player.NickName = pseudo;
		AutoConnect = true;
	}
}
