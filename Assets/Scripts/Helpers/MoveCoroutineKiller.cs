using UnityEngine;
using System.Collections;

public class MoveCoroutineKiller : Singleton<MoveCoroutineKiller> {
	protected MoveCoroutineKiller () {}
	
	private bool _locked = false;

	public void kill() {
		this._locked = true;
	}

	public bool isKilled() {
		if(this._locked == true) {
			this._locked = false;
			return true;
		} else {
			return false;
		}
	}
}