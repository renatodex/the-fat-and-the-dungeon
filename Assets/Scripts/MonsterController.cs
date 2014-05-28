using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterController : MonoBehaviour {

	public string _name;
	public int _hp;
	public int _attack_power;
	public int _defense_power;
	public float _attack_weakness_fart;
	public float _attack_weakness_pose;
	public float _attack_weakness_food;
	public float _attack_weakness_scaryface;


	public NpcAttributes _npc;
	private PlayerInfo _playerInfo;
	private int _action_selector;

	private List<Animator> _hudActionsMapper;
	private List<string> _leroyAnimationMapper;
	private List<AudioSource> _hudActionSoundMapper;


	public NpcAttributes getNpc() {
		return this._npc;
	}


	// Use this for initialization
	void Awake () {
		Debug.Log ("battle sights start ");


		this._npc = new NpcAttributes();
		this._npc._name = this._name;
		this._npc._hp = this._hp;
		this._npc._attack_power = this._attack_power;
		this._npc._defense_power = this._defense_power;


		StartCoroutine("sp_BattleSight");
		this._playerInfo = new PlayerInfo();

		this._hudActionsMapper = new List<Animator>();
		this._hudActionsMapper.Add (GameObject.Find ("Attack_ScaryFace").GetComponent<Animator>());
		this._hudActionsMapper.Add (GameObject.Find ("Attack_Food").GetComponent<Animator>());
		this._hudActionsMapper.Add (GameObject.Find ("Attack_Pose").GetComponent<Animator>());
		this._hudActionsMapper.Add (GameObject.Find ("Attack_Fart").GetComponent<Animator>());

		this._leroyAnimationMapper = new List<string>();
		this._leroyAnimationMapper.Add ("attack_scaryface");
		this._leroyAnimationMapper.Add ("attack_food");
		this._leroyAnimationMapper.Add ("attack_pose");
		this._leroyAnimationMapper.Add ("attack_fart");

		BattleActionsController battleActionController = Pick.Instance.getBattleActionsController();
		this._hudActionSoundMapper = new List<AudioSource>();
		this._hudActionSoundMapper.Add (battleActionController._AttackScaryFace);
		this._hudActionSoundMapper.Add (battleActionController._AttackFood);
		this._hudActionSoundMapper.Add (battleActionController._AttackPose);
		this._hudActionSoundMapper.Add (battleActionController._AttackFart);
	}

	private Vector3 getBottomPosition() {
		Vector3 floorPosition = GameObject.FindGameObjectWithTag("Floor").transform.position;
		return new Vector3(transform.position.x, floorPosition.y, transform.position.z);
	}

	IEnumerator sp_BattleSight() {
		while(true) {

			if(Physics2D.Raycast(transform.position, Vector3.left, 12f)) {
				Debug.DrawRay(transform.position, Vector3.left*12f, Color.green, 1f);
				Debug.Log ("battle!");
				StartCoroutine("sp_Battle");
				yield break;
			} else {
				Debug.DrawRay(transform.position, Vector3.left*12f, Color.red, 1f);
			}
			yield return null;
		}
	}

	private void selectorChange() {
		Pick.Instance.getBattleActionsController()._BlipSound.Play();
	}

	private void decreaseSelector() {
		this.selectorChange();
		if(this._action_selector > 0) {
			this._action_selector--;
		}
	}

	private void increaseSelector() {
		this.selectorChange();
		if(this._action_selector < 3) {
			this._action_selector++;
		}
	}

	IEnumerator battleStartup() {
		Debug.Log ("The Battle has Started!");
		this._playerInfo.getScript().setDisable(true);
		yield return new WaitForSeconds(0.1f);
		Pick.Instance.getBattleActions().position = this._playerInfo.getScript().getHeadPosition();
		this._action_selector = 0;
		this.updateBattleIconHover();

		this.getNpc().prepareToBattle();
		this._playerInfo.getNpc().prepareToBattle();

		Pick.Instance.getGameController().dungeonSound.Stop();
		Pick.Instance.getGameController().battleSound.Play();

		Pick.Instance.getHpBar().renderer.enabled = true;
		Pick.Instance.getHpBar().transform.position = this.getBottomPosition();

		Pick.Instance.getHpBarController().updateBar(this.getNpc().getHpPercentage());

		yield return null;
	}

	IEnumerator sp_Battle() {
		StartCoroutine("battleStartup");

		while(true) {

			if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				this.decreaseSelector();
				this.updateBattleIconHover();
			}
			if(Input.GetKeyDown(KeyCode.RightArrow)) {
				this.increaseSelector();
				this.updateBattleIconHover();
			}
			if(Input.GetKeyDown(KeyCode.Return)) {
				Debug.Log ("Tecla pressionada");

				Pick.Instance.getBattleActionsController()._SelectAttack.Play();

				this._hudActionSoundMapper[this._action_selector].Play();

				this._playerInfo.getAnimator().SetBool(this._leroyAnimationMapper[this._action_selector], true);
				yield return new WaitForSeconds(1f);
				this._playerInfo.getAnimator().SetBool(this._leroyAnimationMapper[this._action_selector], false);

				StartCoroutine("sp_ExecuteAttack");
			
			}

			if(this.getNpc().isFainted()) {
				this._playerInfo.getScript().setDisable(false);
				GameObject.Destroy(this.gameObject);
				Pick.Instance.getBattleActions().position = new Vector3(-999f,-999f,0f);

				Pick.Instance.getGameController().dungeonSound.Play();
				Pick.Instance.getGameController().battleSound.Stop();

				Pick.Instance.getHpBar().renderer.enabled = false;

				Pick.Instance.getBattleActionsController()._MonsterKill.Play();

				yield break;
			}
		
			yield return null;
		}
	}

	IEnumerator sp_ExecuteAttack() {
		Debug.Log ("Ataque executando..");

		this._playerInfo.getScript().Attack(this._action_selector, this);

		Pick.Instance.getBattleActionsController()._MonsterDamage.Play();

		Debug.Log ("Porcentagem de HP restante: " + this.getNpc().getHpPercentage());

		Pick.Instance.getCameraController().doTheShake();

		Pick.Instance.getHpBarController().updateBar(this.getNpc().getHpPercentage());

		yield return null;
	}

	public float getWeaknessByChoosenAttack(int attack) {
		float weakness = 0f;

		switch(attack) {
			case 0:
				weakness = this._attack_weakness_scaryface;
				break;
			case 1:
				weakness = this._attack_weakness_food;
				break;
			case 2:
				weakness = this._attack_weakness_pose;
				break;
			case 3:
				weakness = this._attack_weakness_fart;
				break;
		}

		return weakness;
	}

	void updateBattleIconHover() {
		this.changeBattleIconsToIddle();
		this._hudActionsMapper[this._action_selector].SetBool("hover", true);
	}

	void changeBattleIconsToIddle() {
		foreach(Animator anim in this._hudActionsMapper) {
			anim.SetBool("hover", false);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
