using UnityEngine;
using System.Collections;

public class NpcAttributes
{
	public string _name;
	public int _hp;
	public int _attack_power;
	public int _defense_power;

	private int _currentHp = 1;

	public void prepareToBattle() {
		this._currentHp = this._hp;
	}

	public int getCurrentHp() {
		return this._currentHp;
	}

	public bool isFainted() {
		return this.getCurrentHp() == 0;
	}

	public void hit(int damage) {
		int final_damage = damage - _defense_power;

		if(this._currentHp-final_damage < 0) {
			this._currentHp = 0;
		} else {
			this._currentHp -= final_damage;
		}
	}
}

