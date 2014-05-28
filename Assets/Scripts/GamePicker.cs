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

	public Transform getHpBar() {
		return GameObject.FindGameObjectWithTag("HpBar").transform;
	}

	public HpBarController getHpBarController() {
		return Pick.Instance.getHpBar().FindChild("Amount").GetComponent<HpBarController>();
	}

	public CameraController getCameraController() {
		return Camera.main.GetComponent<CameraController>();
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

