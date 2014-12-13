using UnityEngine;
using System.Collections;

/**
 * Se utiliza para reflejar el tiempo de vida de una flecha.
 */

public class Arrow : MonoBehaviour {

	public Vector3 direction = Vector3.down;

	public float speed = 10.0f;

	public float lifetime = 10.0f;
	
	void Start () {
		//Invoca al metodo de destruir cuando se acaba su tiempo de vida
		Invoke ("DestroyMe", lifetime);
	}

	void FixedUpdate () {
		transform.position += direction * speed * Time.deltaTime;
	}

	void DestroyMe()
	{
		Destroy(gameObject);
	}
}
