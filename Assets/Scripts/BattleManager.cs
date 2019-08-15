using UnityEngine;

namespace Amaze {
    public class BattleManager : MonoBehaviour {

        private void Awake() {
            if (!PhotonNetwork.connected) {
                PhotonNetwork.offlineMode = true;
                RoomOptions localRoomOptions = new RoomOptions {
                    IsVisible = false,
                    IsOpen = false,
                    MaxPlayers = 1
                };
                PhotonNetwork.CreateRoom("LocalRoom", localRoomOptions, TypedLobby.Default);
            }
        }

        private void Start() {
            PhotonNetwork.Instantiate("PlayerCharacter", Vector3.zero, Quaternion.identity, 0);
        }
    }
}
