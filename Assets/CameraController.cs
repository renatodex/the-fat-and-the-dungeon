using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void doTheShake() {
		StartCoroutine("Shake");
	}

	IEnumerator Shake() {

		float magnitude = 0.5f;
		float duration = 0.1f;
		float elapsed = 0.0f;
		
		Vector3 originalCamPos = Camera.main.transform.position;

		Vector3 playerPosition = Pick.Instance.getPlayer().position;
		playerPosition.z = Camera.main.transform.position.z;

		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;          

			float x = Random.Range(playerPosition.x -0.5f, playerPosition.x +0.5f) +6f;//Random.value * playerPosition.x;//Random.value * 2.0f - 1.0f;
			float y = Random.Range(playerPosition.y -0.5f, playerPosition.y +0.5f);//Random.value * playerPosition.x;//Random.value * 2.0f - 1.0f;
			
			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
			
			yield return null;
		}
		
		Camera.main.transform.position = originalCamPos;
	}
}
