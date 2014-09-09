using UnityEngine;
using System.Collections;

public class GUINetwork : Photon.MonoBehaviour {
	public GUIText gt_PlayerOneHealth;
	public GUIText gt_PlayerTwoHealth;

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (gt_PlayerOneHealth.text);
			stream.SendNext (gt_PlayerTwoHealth.text);
		} else {
			if (photonView.isMine) {
				gt_PlayerOneHealth.text = (int)stream.ReceiveNext();
			}
		}
	}
}
