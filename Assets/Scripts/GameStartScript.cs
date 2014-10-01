using UnityEngine;
using System.Collections;

public class GameStartScript : Photon.MonoBehaviour {

	public GameObject gameStartPrefab;
	public GameObject threePrefab;
	public GameObject twoPrefab;
	public GameObject onePrefab;

	public static int count;

	public static bool start = false;

	// Use this for initialization
	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(start)
		{
			if(count < 180)
			{
				gameStartPrefab.renderer.enabled = true;
				if(count < 60)
				{
					threePrefab.renderer.enabled = true;
					twoPrefab.renderer.enabled = false;
					onePrefab.renderer.enabled = false;
					float size = 10 - count/6f + 6f;
					threePrefab.transform.localScale = new Vector3(size, size, 0);
				}
				else if(count < 120)
				{
					threePrefab.renderer.enabled = false;
					twoPrefab.renderer.enabled = true;
					onePrefab.renderer.enabled = false;
					float size = 10 - (count%60)/6f + 6f;
					twoPrefab.transform.localScale = new Vector3(size, size, 0);
				}
				else if(count < 180)
				{
					threePrefab.renderer.enabled = false;
					twoPrefab.renderer.enabled = false;
					onePrefab.renderer.enabled = true;
					float size = 10 - (count%60)/6f + 6f;
					onePrefab.transform.localScale = new Vector3(size, size, 0);
				}
				count++;
			}
			else if(count == 180)
			{
				this.photonView.RPC("startPlayer", PhotonTargets.All, null);
				count++;
			}
			else
			{
				threePrefab.renderer.enabled = false;
				twoPrefab.renderer.enabled = false;
				onePrefab.renderer.enabled = false;
				gameStartPrefab.renderer.enabled = false;
			}
		}
	}

	[RPC] void startPlayer()
	{
		PlayerScript.start = true;
	}
}
