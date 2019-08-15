using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Chat : Photon.MonoBehaviour {

	public ScrollRect myScrollRect;
	public InputField TextSend;
	public Text TextChat;
	public GameObject TextSendObj;
	public string pseudo;

	public bool isSelect = false;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) && !isSelect) {
			//EventSystem.current.SetSelectedGameObject (TextSendObj.gameObject, null);
			EventSystem.current.SetSelectedGameObject (TextSend.gameObject, null);
			isSelect = true;
		} else if (Input.GetKeyDown (KeyCode.Return) && isSelect && TextSend.text.Length > 0) {
			isSelect = false;
			string msg = TextSend.text;
			EventSystem.current.SetSelectedGameObject (null);
			sendChatOfMaster (msg);
			TextSend.text = "";
		} else if(Input.GetKeyDown (KeyCode.Return) && isSelect && TextSend.text.Length == 0){
			isSelect = false;
			//TextSendObj.SetActive (false);
			EventSystem.current.SetSelectedGameObject (null);
			TextSend.text = "";
		}
	}

	public void sendChatOfMaster(string msg){
		if (msg != "") {
			PhotonPlayer photon = PhotonNetwork.player;
			bool isMaster = false;
			if (photon.IsMasterClient) {
				isMaster = true;
			}
			photonView.RPC ("sendChatMaster", PhotonTargets.MasterClient, isMaster, msg, globalVariables.pseudo);
			TextSend.text = "";
		}
	}

	public void sendChatOfMasterViaBtn(){
		string msg = TextSend.text;
		if (msg != "") {
			PhotonPlayer photon = PhotonNetwork.player;
			bool isMaster = false;
			if (photon.IsMasterClient) {
				isMaster = true;
			}
			photonView.RPC ("sendChatMaster", PhotonTargets.MasterClient, isMaster, msg, globalVariables.pseudo);
			TextSend.text = "";
		}
	}

	[PunRPC]
	public void sendChatMaster(bool master, string msg, string pse){
		if (PhotonNetwork.isMasterClient) {
			photonView.RPC ("SendMsg", PhotonTargets.All, master, msg, pse);
		}
	}

	[PunRPC]
	public void SendMsg(bool master, string msg, string pse){
		if (master) {
			TextChat.text += "<color=#a52a2aff>" + pse + " : </color><color=#ffffffff>" +msg+ "</color>\n"; 
		} else {
			TextChat.text += "<color=#add8e6ff>" + pse + " : </color><color=#ffffffff>" +msg+ "</color>\n"; 
		}
		myScrollRect.verticalNormalizedPosition = 0f;
	}

	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{    
		SendMsgConnectionMaster(player.NickName);
	}

	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
        SendMsgDisconnectedMaster(player.NickName);	}

	public void SendMsgConnection(string pse)
	{
		photonView.RPC ("SendMsgConnectionMaster", PhotonTargets.MasterClient, globalVariables.pseudo);
	}

	[PunRPC]
	public void SendMsgConnectionMaster(string pse)
	{
		if (PhotonNetwork.isMasterClient) {
			photonView.RPC ("SendMsgConnectionAll", PhotonTargets.All, pse);
		}
	}

	[PunRPC]
	public void SendMsgDisconnectedMaster(string pse)
	{
		if (PhotonNetwork.isMasterClient)
		{
			photonView.RPC("SendMsgDisconnectionAll", PhotonTargets.All, pse);
		}	}

	[PunRPC]
	public void SendMsgConnectionAll(string pse)
	{
		TextChat.text += "<color=#add8e6ff><i>" + pse + "</i></color><color=#ffa500ff><i> entered in the room.</i></color>\n"; 
	}

	[PunRPC]
	public void SendMsgDisconnectionAll(string pse)
	{
		TextChat.text += "<color=#add8e6ff><i>" + pse + "</i></color><color=#ffa500ff><i> leave the room.</i></color>\n";	}

	public void SelectInputByClick(){
		if (!isSelect) {
			isSelect = true;
		}
	}
		
}
