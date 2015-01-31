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
	public GameObject projectile;

	//Hay que crear un objeto vacio hijo dentro del player que sera la posicion de disparo
	public GameObject gunPosition = null;

	public Camera camera1 = null;

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
	//	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	//	RaycastHit hit;
	//	if (Physics.Raycast(ray, out hit, 100))
	//		Debug.DrawLine(ray.origin, hit.point);

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
		if (Input.GetButtonDown("Fire1") && weaponsActive) 
		{	
			Debug.Log ("Disparo");

			RaycastHit2D hit2d;

			Vector2 mouse = new Vector2(camera1.ScreenToWorldPoint(Input.mousePosition).x, camera1.ScreenToWorldPoint(Input.mousePosition).y);
			Vector2 pj = new Vector2(transform.position.x, transform.position.y);
			hit2d = Physics2D.Raycast(mouse, pj);
			if (hit2d.collider != null)
			{
				Debug.Log ("Colision");
			}
			//Debug.Log ("Raton: " + mouse.ToString());
			//Debug.Log ("PJ: " + transform.position.ToString());

			Vector2 dir = mouse - pj;
			dir.Normalize();

			Vector2 punto_inferior = new Vector2(transform.position.x, transform.position.y - 5);
			float angulo = -45;

			Vector2 dir_inferior = punto_inferior - pj;
			dir_inferior.Normalize();

			angulo = Vector2.Angle(dir, dir_inferior);

			//Debug.Log ("Angulo: " + angulo.ToString());

			//Crea nueva instancia de la flecha
			GameObject arrow = Instantiate(projectile, gunPosition.transform.position, transform.rotation) as GameObject;
			Physics2D.IgnoreCollision(arrow.collider2D, this.collider2D);
			arrow.rigidbody2D.velocity = new Vector2(dir.x * 3, dir.y * 3);
			//arrow.velocity = new Vector2(dir.x * 3, dir.y * 3);
			if (mouse.x > pj.x) 
				arrow.transform.Rotate(Vector3.forward * angulo);
			else
				arrow.transform.Rotate(Vector3.forward * -angulo);

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
