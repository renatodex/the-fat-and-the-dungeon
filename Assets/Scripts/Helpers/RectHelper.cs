using UnityEngine;
using System.Collections;

public static class RectHelper
{
	public static Rect centerize(float width, float height) {
		float dialog_x = (Screen.width/2-width/2);
		float dialog_y = (Screen.height/2-height/2);
		return new Rect(dialog_x,dialog_y, width, height);
	}
}

