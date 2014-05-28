using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public AudioSource battleSound;
	public AudioSource dungeonSound;

	// Use this for initialization
	void Start () {
		this.dungeonSound.Play();
		//this.dungeonSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
