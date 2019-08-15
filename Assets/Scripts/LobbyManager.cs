using UnityEngine;
using UnityEngine.UI;

namespace Amaze {
    public class LobbyManager : MonoBehaviour {

        [SerializeField] private Button battleButton;

        private void Awake() {
            battleButton.onClick.AddListener(OnBattleButtonClick);
            if (PhotonNetwork.isMasterClient) {
                battleButton.gameObject.SetActive(true);
            } else {
                battleButton.gameObject.SetActive(false);
            }
        }

        private void OnBattleButtonClick() {
            Debug.Log("OnBattleButtonClick");
            PhotonNetwork.LoadLevel("Battle");
        }
    }
}
