using UnityEngine;

namespace Amaze {
    public class PlayerCharacter : Photon.PunBehaviour {

        [SerializeField] private float maxSpeed;
        private CharacterController cc;

        private void Awake() {
            cc = GetComponent<CharacterController>();
        }

        private void Update() {
            if (!photonView.isMine) {
                return;
            }
            float hori = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            cc.SimpleMove(Vector3.ClampMagnitude(new Vector3(hori, 0f, vert), 1f) * maxSpeed);
        }
    }
}
