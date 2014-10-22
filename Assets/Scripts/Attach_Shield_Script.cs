using UnityEngine;
using System.Collections;

public class Attach_Shield_Script : Photon.MonoBehaviour {

	// Use this for initialization
	public Transform target;
	public float xOffset;
	private Transform thisTransform;
	private Vector2 velocity;
	public int isActive = 0;

	void Start () {
		thisTransform = transform;
		thisTransform.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
		{
			if (isActive > 0) {
				thisTransform.GetComponent<SpriteRenderer>().enabled = true;
				Vector2 temp = thisTransform.position;
				temp.x = target.position.x + xOffset;
				temp.y = target.position.y;
				
				thisTransform.position = temp;
				isActive--;
			}

			if (isActive == 0) {
				thisTransform.GetComponent<SpriteRenderer>().enabled = false;
			}
		}

	}
}
