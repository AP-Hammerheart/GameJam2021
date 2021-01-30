using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Floor FloorPrefab;

	private Floor FloorInstance;

	private void Start () {
		BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private void BeginGame () {
		FloorInstance = Instantiate(FloorPrefab) as Floor;
		StartCoroutine(FloorInstance.GenerateMap());
	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(FloorInstance.gameObject);
		BeginGame();
	}
}