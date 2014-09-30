using UnityEngine;
using System.Collections;

public class RecycleObjectScript : MonoBehaviour {

	private bool hasSpawn;
	void Start() 
	{
		hasSpawn = false;
	}

	// Update is called once per frame
	void Update () {
		if(hasSpawn == false)
		{
			if (renderer.IsVisibleFrom(Camera.main))
			{
				hasSpawn = true;
			}
		}
		else
		{
			if (renderer.IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
	}
}
