using UnityEngine;
using System.Collections;

//Rotura de un objeto prueba, su animador necesita la variable de rotura "isBroken"
public class CollisionTest : MonoBehaviour {

	private Animator animator;
	private CircleCollider2D cc;

	void Start () {
		animator = this.GetComponent<Animator>();
		cc = this.GetComponent<CircleCollider2D>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("Objeto triger entra");
		if(!other.gameObject.name.Equals("Player"))
			if (animator.GetBool ("isBroken") == false) {
				animator.SetBool ("isBroken", true);
				Destroy (other.gameObject);
				cc.enabled = false;
			}
	}
}
