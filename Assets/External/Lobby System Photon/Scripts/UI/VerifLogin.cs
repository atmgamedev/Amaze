using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifLogin : MonoBehaviour {

	public InputField textlogin;
	public Button valider;
	public connexion connPhoton;

	// Update is called once per frame
	void Update () {
		if (textlogin.text.Length > 3) {
			valider.interactable = true;
		} else if (textlogin.text.Length <= 0) {
			valider.interactable = false;
		}
	}

	public void Login(){
		connPhoton.ConnexionLogin (textlogin.text);
	}
}
