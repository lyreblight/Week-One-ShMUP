using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Camera m_MainCamera;

	public GameObject m_PlayerOne;
	public GameObject m_PlayerTwo;

	public int m_StartingAsteroid = 1;
	public GameObject m_AsteroidPrefab;

	private Vector3 m_ScreenSW;
	private Vector3 m_ScreenNE;

	void Start () {
		m_ScreenSW = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, Camera.main.transform.localPosition.y));
		m_ScreenNE = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, Camera.main.transform.localPosition.y));

		for (int foo = 0; foo < m_StartingAsteroid; foo++) {
			Vector3 tempLocation = new Vector3(Random.Range(m_ScreenSW.x, m_ScreenNE.x), Random.Range(m_ScreenSW.y, m_ScreenNE.y), 0.0f);
			Vector3 tempRotation = new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

			Instantiate (m_AsteroidPrefab, tempLocation, Quaternion.Euler(tempRotation));
		}
	}

	void Update () {
		//m_MainCamera.orthographicSize = Mathf.Clamp(Vector3.Distance(m_PlayerOne.transform.position, m_PlayerTwo.transform.position), 4.0f, 7.0f);
	}
}
