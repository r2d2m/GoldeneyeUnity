using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
	
	AudioSource aus;

	/*
	 * song: audio que queremos reproducir
	 * post: se llama a la funcion de reproduccion con el sonido
	 * */
	//Por que necesitamos esta funcion en este caso?
	public void PlaySoundOnce (AudioClip song){
		StartCoroutine (PlaySoundCoroutine (song));
	}

	/*
	 * song: audio que queremos reproducir
	 * post: se reproduce el sonido
	 * */
	//Por que necesitamos este mecanismo para reproducir el sonido en este caso?
	IEnumerator PlaySoundCoroutine(AudioClip sound){
		aus = GetComponent<AudioSource> ();
		aus.PlayOneShot (sound);
		//Cual es la funcion de la siguiente linea?
		yield return new WaitForSeconds (sound.length);
		Destroy (gameObject);
	}
}
