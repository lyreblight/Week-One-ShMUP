using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
	#region Variables
	public GameObject m_BulletPrefab;
	
	public float m_Speed = 100.0f;
	public float m_TurnSpeed = 100.0f;
	public float m_TurnDirection = 0.0f;

	private bool m_IsHit = false;
	
	private Vector3 m_ScreenSW;
	private Vector3 m_ScreenNE;
	private float m_WrapPadding = 1.0f;
	#endregion

	void Start () {
		m_ScreenSW = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, Camera.main.transform.localPosition.y));
		m_ScreenNE = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, Camera.main.transform.localPosition.y));

		m_TurnDirection = Random.Range (-1.0f, 1.0f);

		rigidbody2D.AddForce (transform.up * m_Speed);
	}

	void Update () {
		if (transform.position.x < m_ScreenSW.x - m_WrapPadding) 
			transform.position = new Vector3 (m_ScreenNE.x, transform.position.y, transform.position.z);

		if (transform.position.x > m_ScreenNE.x + m_WrapPadding) 
			transform.position = new Vector3 (m_ScreenSW.x, transform.position.y, transform.position.z);

		if (transform.position.y < m_ScreenSW.y - m_WrapPadding) 
			transform.position = new Vector3 (transform.position.x, m_ScreenNE.y, transform.position.z);
		
		if (transform.position.y > m_ScreenNE.y + m_WrapPadding) 
			transform.position = new Vector3 (transform.position.x, m_ScreenSW.y, transform.position.z);

		transform.localEulerAngles = new Vector3 (0.0f, 0.0f, (transform.localEulerAngles.z + ((m_TurnSpeed * Time.deltaTime) * m_TurnDirection)));
	}
}
