using UnityEngine;
using System.Collections;

public class LaserController : Photon.MonoBehaviour {
	#region Variables
	public bool b_IsRemoteObject = false;

	public float m_Speed = 100.0f;
	
	private bool m_IsHit = false;
	
	private Vector3 m_ScreenSW;
	private Vector3 m_ScreenNE;
	private float m_DestroyPadding = 1.0f;
	#endregion

	void Start () {
		rigidbody2D.AddForce (transform.up * m_Speed);
	}

	[RPC]
	void SendDestroyMessage() {
		PhotonNetwork.Destroy (gameObject);
    }

	void Update () {
		if (b_IsRemoteObject)
			return;

		m_ScreenSW = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, Camera.main.transform.localPosition.y));
		m_ScreenNE = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, Camera.main.transform.localPosition.y));

		if (transform.localPosition.x < m_ScreenSW.x - m_DestroyPadding ||
			transform.localPosition.x > m_ScreenNE.x + m_DestroyPadding ||
			transform.localPosition.y < m_ScreenSW.y - m_DestroyPadding ||
			transform.localPosition.y > m_ScreenNE.y + m_DestroyPadding)
			photonView.RPC ("SendDestroyMessage", PhotonTargets.All);
	}

	void OnTriggerEnter2D (Collider2D collidingObject) {
		if (b_IsRemoteObject)
			return;

		if (this.tag == "PlayerOneBullet" && collidingObject.tag == "PlayerTwo") {
			photonView.RPC ("SendDestroyMessage", PhotonTargets.All);

			ShipController tempPlayerTwoController = collidingObject.gameObject.GetComponent<ShipController>();
			tempPlayerTwoController.m_CurrentHealth -= 1;
		} else
		if (this.tag == "PlayerTwoBullet" && collidingObject.tag == "PlayerOne") {
			photonView.RPC ("SendDestroyMessage", PhotonTargets.All);
			
			ShipController tempPlayerOneController = collidingObject.gameObject.GetComponent<ShipController>();
			tempPlayerOneController.m_CurrentHealth -= 1;
		}
	}
}
