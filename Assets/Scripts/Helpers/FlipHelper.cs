using UnityEngine;
using System.Collections;

public static class FlipHelper
{
	public static void flip(Transform transform) {
		transform.localScale = new Vector3(
			transform.localScale.x * -1,
			transform.localScale.y,
			transform.localScale.z
		);
	}

	public static void flipLeft(Transform transform) {
			FlipHelper._flip(transform, 0); 
	}

	public static void flipRight(Transform transform) {
			FlipHelper._flip (transform, 180);
	}

	public static void _flip(Transform transform, int value) {
		transform.eulerAngles = new Vector3(0,value,0);
	}


}

