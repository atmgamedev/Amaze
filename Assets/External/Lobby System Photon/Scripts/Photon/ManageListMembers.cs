using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class ManageListMembers : Photon.MonoBehaviour {

	public static ManageListMembers instance = null;
	public List<MPPlayer> playersList = new List<MPPlayer> ();
	public int PlayersInRoom;
	public Text ListMemberText;
	public Text TitleRoom;

	// Use this for initialization

	void Awake(){
		instance = this;
	}

	public void launch () {
		Join (PhotonNetwork.player, globalVariables.pseudo);
		TitleRoom.text = "Table N°" + PhotonNetwork.room.Name;
	}

	//SEND TO MASTER CLIENT FOR YOUR ADD TO PLAYERS LIST
	public void Join(PhotonPlayer photon, string pseudo){
		globalVariables.Myglobalid = PhotonNetwork.room.PlayerCount;
		bool isMaster = false;
		if (photon.IsMasterClient) {
			isMaster = true;
		}
		photonView.RPC("CmdAddPlayerToList", PhotonTargets.MasterClient, photon, pseudo, isMaster, globalVariables.Myglobalid);
	}

	[PunRPC]
	//IF MASTERCLIENT ADD IN YOUR MEMBERS LIST, NEW PLAYER CONNECTED AND LAUNCH "RpcAddPlayerToList"
	public void CmdAddPlayerToList(PhotonPlayer uniqueID, string pse, bool master, int globalID){
		if (!PhotonNetwork.isMasterClient) {
			return;
		}
		if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers) {
			PhotonNetwork.room.IsVisible = false;
			PhotonNetwork.room.IsOpen = false;
		}
		MPPlayer addlist = new MPPlayer();
		addlist.id = globalID;
		addlist.pseudo = pse;
		addlist.Master = master;
		addlist.IDphoton = uniqueID;
		playersList.Add (addlist);

		PlayersInRoom = PhotonNetwork.room.PlayerCount;

		if (master) {
			pse = "<color=#a52a2aff>" + pse + "</color>";
		}
		ListMemberText.text += pse + "\n";
		RpcAddPlayerToList ();
	}

	//IF MASTERCLIENT, SEND YOUR LIST MEMBERS TO ALL CLIENT
	public void RpcAddPlayerToList(){
		photonView.RPC("RpcClearList", PhotonTargets.Others);
		foreach (MPPlayer p in playersList) {
			photonView.RPC("RpcAddPlayerToList2", PhotonTargets.Others, p.pseudo, p.IDphoton, p.Master, p.id);
		}
	}

	[PunRPC]
	//ADD NEW PLAYERS TO LIST MEMBERS
	public void RpcAddPlayerToList2(string playerName, PhotonPlayer photonID, bool isMaster, int idplayer){
		if (PhotonNetwork.isMasterClient) {
			return;
		}
		MPPlayer addlist = new MPPlayer ();
		addlist.id = idplayer;
		addlist.pseudo = playerName;
		addlist.Master = isMaster;
		addlist.IDphoton = photonID;
		playersList.Add (addlist);
		if (isMaster) {
			playerName = "<color=#a52a2aff>" + playerName + "</color>";
		}
		ListMemberText.text += playerName + "\n";
	}

	[PunRPC]
	//ERASE THE LIST MEMBERS TO ALL CLIENTS EXCEPT MASTER
	public void RpcClearList(){
		if (PhotonNetwork.isMasterClient) {
			return;
		}
		playersList.Clear ();
		ListMemberText.text = "";
	}

	[PunRPC]
	//REFRESH LIST PLAYERS AFTER PLAYER DISCONNECTED
	public void CheckList(PhotonPlayer idview){
		if (PhotonNetwork.isMasterClient) {

			if (PhotonNetwork.room.PlayerCount < PhotonNetwork.room.MaxPlayers) {
				PhotonNetwork.room.IsVisible = true;
				PhotonNetwork.room.IsOpen = true;
			}

			MPPlayer tempPlayer = null;
			foreach (MPPlayer player in playersList) {
				if (player.IDphoton == idview) {
					tempPlayer = player;
				}
			}
			if (tempPlayer != null) {
				playersList.Remove (tempPlayer);
			}

			PlayersInRoom = PhotonNetwork.room.PlayerCount;

			ListMemberText.text = "";

			for (int i = 0; i < PlayersInRoom; i++) {
				string pse = "";
				playersList [i].id = i + 1;
				if (playersList [i].IDphoton.IsMasterClient) {
					playersList [i].Master = true;
					pse = "<color=#a52a2aff>" + playersList [i].pseudo + "</color>";
				} else {
					playersList [i].Master = false;
					pse = playersList [i].pseudo;
				}
				ListMemberText.text += pse + "\n";
			}
			TitleRoom.text = "Table N°" + PhotonNetwork.room.Name;
			RpcAddPlayerToList ();
		}
	}



	//IF PLAYER DISCONNECTED, REFRESH LIST PLAYERS
	public void OnPhotonPlayerDisconnected(PhotonPlayer player){    
		CheckList(player);
	}

	public void OnDisconnectedFromPhoton(){    
		playersList.Clear ();
		ListMemberText.text = "";
	}

}

[System.Serializable]
public class MPPlayer
{
	public int id;
	public string pseudo;
	public bool Master = false;
	public PhotonPlayer IDphoton;
}