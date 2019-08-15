using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thisRoom : Photon.MonoBehaviour {

	public string roomName;
	public Text slogan;
	public GetAvatarPlayerInRoom avat;

	public void JoinSpecificRoom(){
		PhotonNetwork.JoinRoom (roomName);
	}
}
