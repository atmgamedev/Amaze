Thank for purchase!




//!!IMPORTANT!!///////////////////////////////
You need download "Photon Unity Networking" in the Asset store for this asset if not big errors appears!




//!!IMPORTANT!!///////////////////////////////
For joining lobby in photon, you must have "PhotonNetwork.autoJoinLobby" on "TRUE". Look "Scripts->Photon->connexion" line 20.




//!!CHANGE MAX PLAYERS BY ROOMS!!/////////////
Load the scene "index" in the folder named "Scene".
For change the number Maximum of player by room, go to the Hierarchy in asset and look the GameObject named "GetRoomList". 
Look Component "GetRoomList.cs" in "GetRoomList" and change variable "MaxPlayer". Then go to folder named "Resources" and drag and drop of the gameObject 
named "Btn_Room" to Hierarchy in "Canvas->Table_panel->Scroll_View_Table->Viewport->Content_Room_panel". 
Then open the droped gameObject named "Btn_Room" and look in "PlayerInRoom" and create or delete the gameObjects for by your number of maximum 
player in room. Example, for 6 players, delete the gameObjects "P8 and P7" so that only "06" gameObject in present, "P1, P2, P3, P4, P5 and P6". 
06 players = 06 gameobjects or 04 players = 04 gameobjects, etc.... Now add GetComponents "Image" of all "P1, P2, P3, P4, P5 or P6" to 
GetComponent "GetAvatarPlayerInRoom" in "PlayerInRoom" in Array "Avatar". Example, avatar[0] = P1; avatar[1] = P2; avatar[2] = P3; etc... 
Now, ajust the size Width and Height of the window of gameobject "PlayerInRoom" for adapt to size of the gameObject "Btn_Room". Is finish? 
Ok, now Drag and drop your gameobject named "Btn_Room" of the Hierarchy to the folder named "Resources" and overwrite existing "Btn_Room" 
for saving change.




//!!DISPLAY & HIDES UI!!////////////////////////
Go to hierarchy and look gameObject named "GlobalVariable&UI". GetComponent "GlobalUI.cs" for display or hides all panel of UI, and 
"globalVariables.cs" for saved your pseudo and ID.




//!!LOGIN PANEL!!/////////////////////////////
To modify login panel, go to Hierarchy and look in "Canvas->Login_panel".
To modify the numbers minimum the letters for login, go to folder "Scripts->UI" and look in "VerifLogin.cs" and look line 14, 
change "3" by "5" for 5 letters minimum for "Button is interactable is "true". Line 21 in "VerifLogin.cs" is for validate your 
login with "Button" (see "Validate" Button in hierarchy "Canvas->Login_panel->Login->Validate").




//!!LOGIN!!///////////////////////////////////
After clicked the "Button" to login panel with your pseudo, launches the command "Login()" line 22 in "VerifLogin.cs" which 
launches the command ConnexionLogin()" with your pseudo in "connexion.cs" line 68. In the command "ConnexionLogin()" you add 
in variables the "globalVariables.pseudo"  and "PhotonNetwork.player.NickName" your pseudo and activate "autoConnect = true" 
to automatically connect in photon lobby because your "PhotonNetwork.autoJoinLobby" is "true" ;)




//!!DISPLAY ROOMS LIST!!////////////////////////
To modify "Rooms list panel", go to Hierarchy and look in "Canvas->Table_panel".
As soon as you are logged in in the Lobby, launches automatically "OnJoinedLobby()" line 40 in "connexion.cs" and with the command 
"GlobalUI.instance.ShowTablePanel ();" line 42, hides the "panel login" and display the panel "List Rooms" which display the rooms available.
Click on the rooms that appear for the reaches it if the number players in the room is not of maximum or create your room on click button 
"Create Room".
To modify UI button "Table", go to asset in folder "Resources" and modify "Btn_Room".




//!!CREATE ROOM!!/////////////////////////////
After clicked the "Create Room",  launches automatically command "createRoom()" in "GetRoomList.cs" line 74.
Line 75 disable button "Create Room" and create room photon with Random name with "Random.Range(0, 9999)".
After create room, you automatically join the room and launches command automatically "OnJoinedRoom()" line 53 in "connexion.cs".
The command "OnJoinedRoom()", line 55, disable panel "Empty room" if active self and hides "List Room panel" and display "Room panel".
Line 59, "ManageListMembers.instance.launch ();" add your pseudo in the members list of the room.
Command "launch()" line 22 to "ManageListMembers.cs".




//!!ROOM PANEL!!//////////////////////////////
As soon in room, your Master!! oh yeah!! Go to "Canvas->Room_panel->ListMember" and look component "ManageListMembers.cs".
ManageListMembers allows to manage the list of members in the room and the posters in the text area.
As I wrote before, join a room launches automatically "launch()" line 22 to "ManageListMembers.cs" and add Room Name.
Line 23, command "Join()" send to Master your pseudo saved in "globalVariables.cs" and your local Photon.player.
Line 30 to 33, verify if you is Master.
Line 34, send all to Master.
Line 39 to 61, the master receives commands and add new player in local in the list of the room and add to "ListMemberText.text" line 55.
Line 40 to 42, only the Master receive:
if (!PhotonNetwork.isMasterClient) {
	return;
}
Line 65, the master orders all members of the room, to delete the list of members, "PhotonTargets.Others".
Line 66 to 68, the master sends his new list of members in the room to all members of the room, "PhotonTargets.Others".
Line 73 to 87, the members receives the new list members in room and add to "ListMemberText.text" line 82.
Line 74 to 76, only the members receives:
if (PhotonNetwork.isMasterClient) {
	return;
}
Line 91 to 97, the command "RpcClearList()" erase all list members in room all members except Master, Line 61.
Line 101 to 138, refresh list members after the disconnection the member with command launches automatically "OnPhotonPlayerDisconnected()" line 143.
Line 147 to 150, launches local command for erase local your members list text in room.




//!!LEAVE BUTTON IN ROOM!!//////////////////////
After click button "LEAVE ROOM", launches command "leaveRoom()" to "GetRoomList.cs" line 85 and you exit the room with command 
"PhotonNetwork.LeaveRoom ();" line 86.
As soon exit room, launches automatically command "OnLeftRoom()" in "connexion.cs" line 62 and automatically join Lobby which 
launches "OnJoinedLobby()" line 40 in "connexion.cs".




//!!CHAT!!//////////////////////////////////////
To modify "Chat", go to hierarchy in "Canvas->Room_panel->Chat" and look component "Chat.cs".
In the component "Chat.cs", the "Update()" function is used to pressed touch "ENTER" for send message to chat.
Line 19, if pressed touche keyboard "ENTER" and "isSelect" is "false" so "EventSystem.current.SetSelectedGameObject (TextSend.gameObject, null);" line 20, 
select the InputText for say.
Line 23, if pressed touche keyboard "ENTER" and "isSelect" is "true" and "InputText.text.Length > 0" so send the text in "InputText.text" to chat.
Line 29, if pressed touche keyboard "ENTER" and "isSelect" is "true" and "InputText.text.Length == 0" so unselected InputText, erase "InputText.text" and
"isSelect" is "false".
Line 27, send text to Master in function "sendChatOfMaster()" line 37.
Line 37 to 47, function send your pseudo and your message to Master with touche keyboard "ENTER".
Line 39 to 43, verify if you is Master.
Line 44, send to Master your pseudo and your message.
Line 49 to 60, function send your pseudo and your message to Master with button "SEND".
Line 63 to 67, Master receives messages and pseudos and the send at all members in room.
Line 79 to 121, send to Master for say in the chat, the new entered connections and the leavings the room.




//!!GETROOMLIST!!///////////////////////////////
Go to the Hierarchy in asset and look the GameObject named "GetRoomList" and look Component "GetRoomList.cs".
Line 26 to 32, StartCoroutine line 23 for show the numberof the members actually in lobby.
Line 34, the function "OnReceivedRoomListUpdate()" refreshes in Photon the list of the Rooms automatically at 5s intervals and send function line 36 
"GetRoom()" for instantiate the gameObject "Btn_Room" with the function "RoomInfo" of Photon.
Line 35, finding all "Btn_Room" with component "thisRoom" for all destroy for refreshes with "OnReceivedRoomListUpdate()".
Component "thisRoom.cs" serves as a relay of the line 52 to 59 for the display the "The Name of room and the number of members in room".