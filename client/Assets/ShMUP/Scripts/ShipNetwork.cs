using UnityEngine;
using System.Collections;

public class ShipNetwork : Photon.MonoBehaviour {
	public ShipController sc_ShipController;

	void Start () {
		sc_ShipController.b_IsRemotePlayer = !photonView.isMine;
	}

	bool receivedHealth = false;
	Vector3 receivedPlayerPos = Vector3.zero;
	Quaternion receivedPlayerRot = Quaternion.identity;
	void Update() {
		if (!photonView.isMine) {
			transform.position = Vector3.Lerp(transform.position, receivedPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, receivedPlayerRot, Time.deltaTime * 5);
		}
	}

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			//stream.SendNext ((bool)sc_ShipController.b_isHit);
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			//receivedHealth = (bool)stream.ReceiveNext();
			receivedPlayerPos = (Vector3)stream.ReceiveNext();
			receivedPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
}
