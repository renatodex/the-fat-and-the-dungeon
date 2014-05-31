using UnityEngine;
using System.Collections;

public class GUILabel : MonoBehaviour {

	public string name;
	public string prefix;
	public string sufix;
	public string defaultValue;

	// Use this for initialization
	void Start () {
		this.setText(this.defaultValue);
	}

	public void setText(string value) {
		GetComponent<UILabel>().text = this.prefix + value + this.sufix;
	}
}
