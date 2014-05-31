using UnityEngine;
using System.Collections;

public class HpBarController : MonoBehaviour {

	private float _maxWidth = 0.67f;
	public Color _colorHealthy;
	public Color _colorInjured;
	public Color _colorCritical;

	public void updateBar(float hpPercentage) {
		Vector3 newScale = transform.localScale;
		newScale.x = this._maxWidth*hpPercentage/100f;
		transform.localScale = newScale;

		this.colorChange(hpPercentage);
	}

	public void colorChange(float percentage) {
		if(percentage == 100f) {
			renderer.material.color = this._colorHealthy;
		}

		if(percentage < 60f) {
			renderer.material.color = this._colorInjured;
		}

		if(percentage < 30f) {
			renderer.material.color = this._colorCritical;
		}
	}
}
