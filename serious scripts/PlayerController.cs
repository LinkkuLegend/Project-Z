using UnityEngine;
using System.Collections;

/**
 * Controla el movimiento del jugador y dispara.
 */

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;

	public int health = 100;

	public float reloadDelay = 0.2f;

	//Hay que indicar el objeto que dispara ¿Cambiarlo en ejecucion para disparar varias cosas?
	public GameObject prefabArrow = null;

	//Hay que crear un objeto vacio hijo dentro del player que sera la posicion de disparo
	public GameObject gunPosition = null;

	private bool weaponsActive = true;

	private Animator animator;
	
	void Start () {
		animator = this.GetComponent<Animator>();
	}

	void Update()
	{
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");

		if (vertical == 0 && horizontal == 0)
			animator.SetBool ("movement", false);
		else
			animator.SetBool ("movement", true);

		if (animator.GetBool ("movement")) {
			if (vertical > 0) {
				animator.SetInteger ("direction", 2);
			} else if (vertical < 0) {
				animator.SetInteger ("direction", 0);
			} else if (horizontal > 0) {
				animator.SetInteger ("direction", 3);
			} else if (horizontal < 0) {
				animator.SetInteger ("direction", 1);
			}
		}
	}
	
	void FixedUpdate () {
		transform.position = new Vector3 ((transform.position.x + Input.GetAxis ("Horizontal") * speed * Time.deltaTime)
		                                  , transform.position.y
		                                  , transform.position.z);
		transform.position = new Vector3 (transform.position.x
		                                  , (transform.position.y + Input.GetAxis ("Vertical") * speed * Time.deltaTime)
		                                  , transform.position.z);
	}

	//Comprueba el boton de disparo
	void LateUpdate()
	{
		if (Input.GetButton ("Fire1") && weaponsActive) 
		{
			//Crea nueva instancia de la flecha
			Instantiate(prefabArrow, gunPosition.transform.position, prefabArrow.transform.rotation);

			weaponsActive = false;

			//Activa el disparo unos segundos despues
			Invoke ("ActivateWeapons", reloadDelay);
		}
	}

	void ActivateWeapons()
	{
		weaponsActive = true;
	}
}
