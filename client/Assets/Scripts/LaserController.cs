using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {
	#region Variables
	public GameObject m_BulletPrefab;

	public float m_Speed = 100.0f;
	
	private bool m_IsHit = false;
	
	private Vector3 m_ScreenSW;
	private Vector3 m_ScreenNE;
	private float m_DestroyPadding = 1.0f;
	#endregion

	void Start () {
		rigidbody2D.AddForce (transform.up * m_Speed);
	}

	void Update () {
		m_ScreenSW = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, Camera.main.transform.localPosition.y));
		m_ScreenNE = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, Camera.main.transform.localPosition.y));

		if (transform.localPosition.x < m_ScreenSW.x - m_DestroyPadding ||
		    transform.localPosition.x > m_ScreenNE.x + m_DestroyPadding ||
		    transform.localPosition.y < m_ScreenSW.y - m_DestroyPadding ||
		    transform.localPosition.y > m_ScreenNE.y + m_DestroyPadding)
			Destroy (gameObject);
	}
}
