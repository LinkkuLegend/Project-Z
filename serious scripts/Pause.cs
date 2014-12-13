using UnityEngine;
using System;

/**
 * Incorpora un sencillo menu de pausa iniciado al pulsar escape (en la configuracion de teclas, cancel es escape)
 */

public class Pause : MonoBehaviour {

	bool paused = false;
	
	void FixedUpdate()
	{
		if(Input.GetButton("Cancel"))
			paused = togglePause();
	}

	//Esta funcion es la que llama al constructor de interfaz
	void OnGUI()
	{
		if(paused)
		{
			GUILayout.Label("Game is paused!");
			if(GUILayout.Button("Click me to unpause"))
				paused = togglePause();
		}
	}
	
	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
}
