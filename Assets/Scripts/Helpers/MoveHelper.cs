using UnityEngine;
using System.Collections;

public static class MoveHelper
{
	public static IEnumerator move(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {

		Vector3 realEndPos = new Vector3(
			endPos.x,
			endPos.y,
			startPos.z
		);

		float rate = 1f/time;
		float i = 0;
		while (i < 1f) {

			if(MoveCoroutineKiller.Instance.isKilled()) {
				yield break;
			} else {
				i += Time.deltaTime * rate;
				thisTransform.position = Vector3.Lerp(startPos, realEndPos, i);
				yield return null; 
			}


		}
	}
}

