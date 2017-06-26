using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	//En caso de que el sonido background no sea global (se escuche a cualquier distancia al mismo volumen)
	//deberiamos ubicar el audiomanager en la posicion en que queramos que sea la fuente del sonido

	public GameObject auxiliar;						//objeto para variables temporales
	public static AudioManager instance = null;		//instancia de AudioManager

	public AudioClip[] sounds;						//array con todos los sonidos que tenemos en el Inspector
	public string[] names;							//array con los nombres de las canciones asociadas
													//en este script esta relacion no es obligada, cuando decimos un nombre de cancion
													//se selecciona la cancion en la misma posicion que la posicion de nuestro string 
													//en el array names


	/* Si queremos que por defecto se reproduzca un sonido, descomentar el Start
	void Start(){
		setAudioManager ("background_sound_name");
		playSound ("simple_sound_name", new Vector3 (0, 0, 0));
	}
	*/


	/*
	 * name: string asociado a la cancion que queremos reproducir
	 * pos: posicion donde queremos reproducir el sonido
	 * post: se reproduce la cancion asociada al nombre "name"
	 * Reproduccion simple, puede haber multiples
	 * */
	public void playSound(string name, Vector3 pos){
		//Comprobamos que el array "names" contenga "name"
		if (System.Array.IndexOf(names,name)>=0) {
			//Creamos el objeto auxiliar que reproducira el sonido
			GameObject inst = Instantiate (auxiliar,pos,Quaternion.identity) as GameObject;
			AudioClip temp = sounds[System.Array.IndexOf(names,name)];
			//Le decimos al objeto auxiliar que reproduzca el sonido
			inst.GetComponent<Audio> ().PlaySoundOnce (temp);
		}
	}


	/*
	 * bgs_name: string asociado a la cancion que queremos reproducir
	 * post: se reproduce la cancion asociada al nombre "bgs_name" de forma continua
	 * Sonido en loop de background, ya que es este mismo controlador el que lo tiene, solo puede reproducirse uno
	 * */
	public void setAudioManager(string bgs_name){
		//Comprobamos que no haya una instancia de AudioManager
		if (instance == null) 
			//Si no hay, decimos que esta es la actual
			instance = this;
		else 
			//Si hay, miramos si es esta o otra
			if (instance != this)
				//Si es otra, la destruimos
				Destroy (gameObject);
		//Decimos por precaucion que no se destruya el gameObject al cargarse un nivel, para que sirve?
		//Para poder conservar el sonido de fondo entre cargas de nivel, cambios de escenas, etc...
		DontDestroyOnLoad (gameObject);

		//Repdroducimos el sonido de fondo:
		//Accedemos al generador de sonido (componente) del gameObject
		AudioSource aus = GetComponent<AudioSource> ();
		//Le asignamos un sonido de la misma manera que en la funcion "playSound" (pero sin comprobacion de errores)
		aus.clip = sounds[System.Array.IndexOf(names,bgs_name)];
		//Le decimos al repoductor que se ejecute en bucle
		aus.loop = true;
		//Reproducimos el sonido
		aus.Play();

		//Podemos ver el nombre del sonido en reproduccion de esta manera
		//Debug.Log (aus.clip.name);
	}
}
