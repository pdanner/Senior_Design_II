using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public Vector2 speed = new Vector2(10, 10);

	public Vector2 direction = new Vector2(-1, 0);
	private int count = 0;
	private int bossCount = 0;
	private int loopCount = 0;
	private int loopCountCount = 0;
	public string moveType = "default";
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(0,0,0);
		float moveUp = 0.5f;
		float moveDown = -0.5f;
		if(moveType.Equals("default"))
		{
			movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
		}
		else if(moveType.Equals("zigzag"))
		{
			count++;			
			if(count%90 <= 45)
			{
				direction = new Vector2(direction.x, 0.5f);
				movement = new Vector3 (speed.x * direction.x, speed.y * moveUp, 0);
			}
			else if(count%90 > 45)
			{
				direction = new Vector2(direction.x, -0.5f);
				movement = new Vector3 (speed.x * direction.x, speed.y * moveDown, 0);
			}
		}
		else if(moveType.Equals ("toptobottom"))
		{
			direction = new Vector2(direction.x, -0.5f);
			movement = new Vector3 (speed.x * direction.x, speed.y * moveDown, 0);
		}
		else if(moveType.Equals ("bottomtotop"))
		{
			direction = new Vector2(direction.x, 0.5f);
			movement = new Vector3 (speed.x * direction.x, speed.y * moveUp, 0);
		}
		else if(moveType.Equals("boss"))
		{
			bossCount++;
			if(bossCount < 60)
			{
				movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
			}
			else
			{
				if(bossCount%180 <= 90)
				{
					direction = new Vector2(0.001f, -0.5f);
				}
				else if(bossCount%180 > 90)
				{
					direction = new Vector2(0.001f, 0.5f);
				}
				movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
			}
		}
		else if(moveType.Equals("loopy"))
		{
			loopCountCount++;
			if(loopCountCount % 5 == 0)
			{
				loopCount++;
			}
			float dir = Mathf.Sin(loopCount) * 8.0f;
			direction = new Vector2(-2.0f, dir);
			movement = new Vector3(speed.x * direction.x, speed.y*direction.y, 0);
		}
		else if(moveType.Equals ("speed"))
		{
			direction = new Vector2(-3.0f, 0.0f);
			movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
		}
		else
		{
			movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
		}
		//Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
		movement *= Time.deltaTime;
		transform.Translate (movement);
	}
}