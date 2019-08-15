using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRoomList : MonoBehaviour {

	public static GetRoomList instance = null;
	public thisRoom[] roomBtn;
	public Button createroom;
	public GameObject btnRoom;
	public GameObject RoomEmpty;
	public RectTransform PositBtnRoom;
	public Text nbrCount;
	public Sprite[] imagesAvatarInRoom;
	public int MaxPlayer = 8;
	public Text TextChat;
	public RoomInfo[] roomInfo;

	void Awake(){
		instance = this;
	}

	void Start(){
		StartCoroutine (CheckMemberInLobby ());
	}

	IEnumerator CheckMemberInLobby(){
		yield return new WaitForSeconds (0.5f);
		int NbrPlayer = PhotonNetwork.countOfPlayersOnMaster;
		nbrCount.text = NbrPlayer.ToString ("00");
		StopCoroutine (CheckMemberInLobby ());
		StartCoroutine (CheckMemberInLobby ());
	}

	public virtual void OnReceivedRoomListUpdate(){
		roomBtn = GameObject.FindObjectsOfType<thisRoom> ();
		GetRoom();
	}

	public void GetRoom(){
		roomInfo = PhotonNetwork.GetRoomList();
		print (roomInfo.Length);
		if (roomInfo.Length > 0) {
			for(int i = 0; i < roomInfo.Length; i++){
				string NameRoom = roomInfo [i].Name;
				string CountPlayerInRoom = roomInfo [i].PlayerCount.ToString("00");
				string MaxPlayersRoom = roomInfo [i].MaxPlayers.ToString("00");
				int CountPlayerInRoomInt = int.Parse (CountPlayerInRoom);
				int MaxPlayersRoomInt = int.Parse (MaxPlayersRoom);
				RoomEmpty.SetActive (false);


				GameObject btn = Instantiate (btnRoom, new Vector3 (0f, 0f, 0f), Quaternion.identity);
				btn.transform.SetParent (PositBtnRoom);
				btn.transform.localScale = new Vector3 (1f, 1f, 1f);
				btn.GetComponentInChildren<Text> ().text = "Table N°" + NameRoom;
				btn.GetComponent<thisRoom> ().roomName = NameRoom;
				btn.GetComponent<thisRoom> ().slogan.text = "Player(s) in Room  -  " + CountPlayerInRoom + "/" + MaxPlayersRoom;
			
				for (int e = 0; e < MaxPlayersRoomInt; e++) {
					if (e < CountPlayerInRoomInt) {
						btn.GetComponent<thisRoom> ().avat.avatar [e].sprite = imagesAvatarInRoom [1];
					} else if (e >= CountPlayerInRoomInt) {
						btn.GetComponent<thisRoom> ().avat.avatar [e].sprite = imagesAvatarInRoom [0];
					}
				}
			}
		} else {
			RoomEmpty.SetActive (true);
			Debug.Log ("Aucune chambre");
		}
		for (int i = 0; i < roomBtn.Length; i++) {
			Destroy (roomBtn[i].gameObject);
		}
	}

	public void createRoom(){
		createroom.interactable = false;
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.maxPlayers = (byte)MaxPlayer;
		PhotonNetwork.CreateRoom (Random.Range(0, 9999).ToString(), roomOptions, null);
	}

	public void leaveRoom(){
		PhotonNetwork.LeaveRoom ();
		TextChat.text = "";
		if (ManageListMembers.instance.playersList.Count > 0) {
			ManageListMembers.instance.playersList.Clear ();
			ManageListMembers.instance.ListMemberText.text = "";
		}
		createroom.interactable = false;
	}
}
