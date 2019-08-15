using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : MonoBehaviour {

	public static GlobalUI instance = null;
	public GameObject TablePanel;
	public GameObject RoomPanel;
	public GameObject LoginPanel;

	void Awake(){
		instance = this;
	}

	public void ShowTablePanel(){
		TablePanel.SetActive (true);
		RoomPanel.SetActive (false);
		LoginPanel.SetActive (false);
	}

	public void ShowRoomPanel(){
		TablePanel.SetActive (false);
		LoginPanel.SetActive (false);
		RoomPanel.SetActive (true);
	}

	public void HideAllPanel(){
		TablePanel.SetActive (false);
		RoomPanel.SetActive (false);
		LoginPanel.SetActive (false);
	}

}
