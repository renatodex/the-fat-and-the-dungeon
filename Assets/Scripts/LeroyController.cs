using UnityEngine;
using System.Collections;

public class LeroyController : MonoBehaviour {

	public string _name;
	public int _hp;
	public int _attack_power;
	public int _defense_power;

	public AudioSource _hittedSound;

	private bool _isDisabled;
	private Animator _animator;
	private NpcAttributes _npc;
	private int _weight;



	public NpcAttributes getNpc() {
		return this._npc;
	}


	// Use this for initialization
	void Start () {
		this._animator = GetComponent<Animator>();

		this._npc = new NpcAttributes();
		this._npc._name = this._name;
		this._npc._hp = this._hp;
		this._npc._attack_power = this._attack_power;
		this._npc._defense_power = this._defense_power;

		this._weight = 200;
		this.updateWeightLabel();

		this.getNpc().init();
	}

	void updateWeightLabel() {
		GUILabel label = Pick.Instance.getLabelByName("player_weight");
		label.setText(this.getWeight().ToString());
	}

	public void looseWeight(int value) {
		this._weight -= value;
		this.updateWeightLabel();

	}

	public void gainWeight(int value) {
		this._weight += value;
		this.updateWeightLabel();
	}

	public int getWeight() {
		return this._weight;
	}

	void OnGUI() {
		/*if(GUILayout.Button("Get Chicken HP")) {
			MonsterController monster = GameObject.Find ("Monster:Chicken").GetComponent<MonsterController>();
			Debug.Log ("Chicken HP: " + monster.getNpc()._hp + "/" + monster.getNpc().getCurrentHp());
		}

		if(GUILayout.Button("Shake Camera")) {
			Camera.main.GetComponent<CameraController>().doTheShake();
		}*/
	}

	void drawButton(string state) {
		if(GUILayout.Button("Switch Attack: "+state)) {
			this._animator.SetBool(state, !this._animator.GetBool(state));
		}
	}

	// Update is called once per frame
	void Update () {
		if(!this._isDisabled) { 
			float move = Input.GetAxis("Horizontal");
			rigidbody2D.velocity = new Vector2(move*5f, 0f);

			Vector3 newScale = transform.localScale;
			if(move > 0) {
				newScale.x = 15;
				GetComponent<Animator>().SetFloat("speed", move);

			} else if(move < 0) {
				newScale.x = -15;

				GetComponent<Animator>().SetFloat("speed", move*-1);
			}

			transform.localScale = newScale;
		}
	}

	public void Attack(int attackType, MonsterController monster) {
		int power = this._npc._attack_power;
		float attack_margin = Random.Range(-0.2f,0f);
		float weakness = monster.getWeaknessByChoosenAttack(attackType);
		int damage_formula = (int) Mathf.Ceil(power + power*attack_margin + power*weakness);

		Debug.Log ("Leroy hitted enemy by "+ damage_formula);

		monster.getNpc().hit(damage_formula);
	}

	public Vector3 getHeadPosition() {
		Vector3 headPosition = transform.position;
		headPosition.y += 0f;
		return headPosition;
	}

	public Vector3 getBottomPosition() {
		Vector3 headPosition = transform.position;
		headPosition.y -= 3.4f;
		return headPosition;
	}

	public void setDisable(bool state) {
		this._isDisabled = state;
		this._animator.SetFloat("speed", 0f);
	}
}
