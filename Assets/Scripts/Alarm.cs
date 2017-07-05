using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour {

	public float HP = 2f;
	public float helpDistance = 50f;

	private AudioSource source;
	private IASoldiers _iaSoldiers;
    private Mission _mision;

	void Start () {
		source = GetComponent<AudioSource>();
        _mision = GameObject.Find("HUD").GetComponent<Mission>();
	}

	public void ActivateAlarm() {
		source.Play();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			if (Vector3.Distance (transform.position, enemy.transform.position) <= helpDistance) {
				_iaSoldiers = enemy.GetComponent<IASoldiers> ();
				_iaSoldiers.RequestHelp();
			}
		}
        _mision.UpdateProgress(0, true);
	}

	public void HitAndDecreaseHP(float damage) {
		HP -= damage;
		if (HP <= 0) {
            _mision.UpdateProgress(0, false);
			source.Stop();
			Destroy(this.gameObject, 0);
		}
	}
}
