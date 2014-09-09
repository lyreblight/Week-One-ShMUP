using UnityEngine;
using System.Collections;

public class NetworkingManager : MonoBehaviour {
	public GameManager sc_GameManager;

	void Awake () {
		//PhotonNetwork.logLevel = PhotonLogLevel.Full;

		if (!PhotonNetwork.connected)
			PhotonNetwork.ConnectUsingSettings ("0.1");
	}

	void OnGUI () {
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby () {
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed () {
		PhotonNetwork.CreateRoom ("ShMUP", true, true, 2);
	}

	void OnJoinedRoom () {
		sc_GameManager.SpawnPlayer (PhotonNetwork.isMasterClient);
	}
}
