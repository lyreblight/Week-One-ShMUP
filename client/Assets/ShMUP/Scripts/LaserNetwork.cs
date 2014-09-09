using UnityEngine;
using System.Collections;

public class LaserNetwork : Photon.MonoBehaviour {
	public LaserController sc_LaserController;

	void Start () {
		sc_LaserController.b_IsRemoteObject = !photonView.isMine;
	}

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			if (photonView.isMine) {
				transform.position = (Vector3)stream.ReceiveNext();
				transform.rotation = (Quaternion)stream.ReceiveNext();
			}
		}
	}
}
