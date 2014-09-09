using UnityEngine;
using System.Collections;

public class ShipController : Photon.MonoBehaviour {
	#region Variables
	public bool b_IsRemotePlayer = false;

	public bool b_isHit = false;

	public int m_CurrentHealth = 100;
	public int m_MaxHealth = 100;

	public float m_Speed = 3.0f;

	public float m_FireRate = 0.1f;
	private float m_LastFire = 0.0f;

	private bool m_IsHit = false;

	private Vector3 m_ScreenSW;
	private Vector3 m_ScreenNE;
	#endregion

	void Start () {
	}

	void Update () {
		if (b_IsRemotePlayer)
			return;

		m_ScreenSW = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, Camera.main.transform.localPosition.y));
		m_ScreenNE = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, Camera.main.transform.localPosition.y));

		Vector3 tempPosition = transform.position;
		if (Input.GetKey (KeyCode.W)) tempPosition.y += (m_Speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.S)) tempPosition.y -= (m_Speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.A)) tempPosition.x -= (m_Speed * Time.deltaTime);
		if (Input.GetKey (KeyCode.D)) tempPosition.x += (m_Speed * Time.deltaTime);
		transform.position = tempPosition;

		m_LastFire += Time.deltaTime;

		if (Input.GetMouseButton (0))
			ShootNormal ();

		Vector3 mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 lookTowards = mouseLocation - transform.position;
		float angle = Mathf.Rad2Deg * Mathf.Atan2 (lookTowards.y, lookTowards.x) - 90;
		this.transform.eulerAngles = new Vector3 (0, 0, angle);
	}

	public void IsHit() {
		Debug.Log ("Is Hit");

		m_CurrentHealth -= 1;
		b_isHit = false;
	}

	[RPC]
	void SendShootNormalMessage() {
		PhotonNetwork.Instantiate (this.tag == "PlayerOne" ? "laserBlue" : "laserRed", transform.position, transform.rotation, 0);
	}

	private void ShootNormal() {
		if (m_IsHit) return;
		
		if (m_LastFire < m_FireRate) return;

		photonView.RPC ("SendShootNormalMessage", PhotonTargets.MasterClient);

		m_LastFire = 0.0f;
	}
}
