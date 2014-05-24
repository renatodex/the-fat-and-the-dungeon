using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

	public string _name;
	public int _hp;
	public int _attack_power;
	public int _defense_power;
	public float _attack_weakness_fart;
	public float _attack_weakness_pose;
	public float _attack_weakness_food;
	public float _attack_weakness_scaryface;


	// Use this for initialization
	void Start () {
		StartCoroutine("sp_BattleSight");
	}

	IEnumerator sp_BattleSight() {
		while(true) {
			Debug.DrawRay(transform.position, Vector3.left*12f, Color.red, 1f);
			if(Physics2D.Raycast(transform.position, Vector3.left, 12f)) {
				Debug.Log ("battle!");
				StartCoroutine("sp_Battle");
				yield break;
			}
			yield return null;
		}
	}

	IEnumerator sp_Battle() {
		while(true) {
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
