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

