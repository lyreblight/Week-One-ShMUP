using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
	#region Variables
	public GameObject m_PlayerReference;
	
	public float m_ShieldTime = 10.0f;
	#endregion

	void Start () {
	
	}

	void Update () {
		transform.localRotation = m_PlayerReference.transform.localRotation;
		transform.position = m_PlayerReference.transform.position;

		m_ShieldTime -= Time.deltaTime;

		if (m_ShieldTime < 0.0f)
			Destroy (gameObject);
	}
}
