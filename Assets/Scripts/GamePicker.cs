using UnityEngine;
using System.Collections;

public class Pick : Singleton<Pick>
{
	protected Pick(){}

	public Transform getPlayer() {
		return GameObject.FindGameObjectWithTag("Player").transform;
	}

	public Transform getBattleActions() {
		return GameObject.FindGameObjectWithTag("BattleActions").transform;
	}

	public BattleActionsController getBattleActionsController() {
		return Pick.Instance.getBattleActions().GetComponent<BattleActionsController>();
	}

	public GameController getGameController() {
		return GameObject.Find ("GameObject").GetComponent<GameController>();
	}

	public Transform getMonsterHpBar() {
		return GameObject.Find("MonsterHpBar").transform;
	}

	public Transform getLeroyHpBar() {
		return GameObject.Find("LeroyHpBar").transform;
	}

	public HpBarController getMonsterHpBarController() {
		return Pick.Instance.getMonsterHpBar().FindChild("Amount").GetComponent<HpBarController>();
	}

	public HpBarController getLeroyHpBarController() {
		return Pick.Instance.getLeroyHpBar().FindChild("Amount").GetComponent<HpBarController>();
	}

	public CameraController getCameraController() {
		return Camera.main.GetComponent<CameraController>();
	}

	public GUILabel getLabelByName(string name) {
		try {
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("GUILabel");
			foreach(GameObject gameObject in gameObjects) {
				if(gameObject.GetComponent<GUILabel>().name == name) {
					return gameObject.GetComponent<GUILabel>();
				}
			}

			throw new UnityException("Could not found Label!");
		} catch(UnityException e) {
			Debug.Log (e.Message);
			return this.getLabelByName("exception_label");
		}
	}
}

public class PlayerInfo
{
	public Transform getTransform() {
		return Pick.Instance.getPlayer();
	}
	
	public LeroyController getScript() {
		return Pick.Instance.getPlayer().GetComponent<LeroyController>();
	}

	public Animator getAnimator() {
		return Pick.Instance.getPlayer().GetComponent<Animator>();
	}

	public NpcAttributes getNpc() {
		return this.getScript().getNpc();
	}
}

