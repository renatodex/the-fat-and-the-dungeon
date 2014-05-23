using UnityEngine;
using System.Collections;

public class LeroyController : MonoBehaviour {

	private Animator _animator;

	// Use this for initialization
	void Start () {
		this._animator = GetComponent<Animator>();
	}

	void OnGUI() {
		drawButton("attack_fart");
		drawButton("attack_pose");
		drawButton("attack_scaryface");
		drawButton("attack_food");
	}

	void drawButton(string state) {
		if(GUILayout.Button("Switch Attack: "+state)) {
			this._animator.SetBool(state, !this._animator.GetBool(state));
		}
	}

	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2(move*5f, 0f);

	

		Vector3 newScale = transform.localScale;
		if(move > 0) {
			newScale.x = 15;
			GetComponent<Animator>().SetFloat("speed", move);

		} else if(move < 0) {
			newScale.x = -15;

			GetComponent<Animator>().SetFloat("speed", move*-1);
		}

		transform.localScale = newScale;
	}
}
