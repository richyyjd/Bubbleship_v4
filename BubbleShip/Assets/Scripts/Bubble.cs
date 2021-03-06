﻿using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour, IBubbleMatrix
{

	public int damage = 1;
	public bool isEnemy = false;
	public bool playerFired = false, oneContact=false;
	public Vector3 speed, direction, rowCol;
	GameController gameController;

	public Enums.BUBBLECOLOR bubbleColor;
	public Sprite[] typeBubbles;

	void Awake ()
	{
		if (!playerFired) {
			speed = new Vector3 (0, 0, 0);
		}
		direction = new Vector3 (1, 1, 0);
		gameController = GameController.Instance ();
		changeColor ((int)bubbleColor);
	}

	public void changeColor(int bubbleCol){
		GetComponent<SpriteRenderer> ().sprite = typeBubbles [bubbleCol];
	}

	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("Antes="+transform.localPosition);
		//Debug.Log ("Ahora="+GameController.Instance().moveToCorrectPosition(transform.localPosition));

		//Debug.Log ("PlayerFired: "+playerFired);
		if (!playerFired)
			gameController.insert (gameObject, false);
			//transform.localPosition = gameController.moveToCorrectPosition (transform.localPosition, false);
		//else Debug.Log ("Speed:"+speed);
	}
		
	// Update is called once per frame
	void Update ()
	{
		Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
		movement *= Time.deltaTime;
			
		transform.Translate (movement);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Bubble scriptBubble = collider.gameObject.GetComponent<Bubble> ();
		if (scriptBubble != null && scriptBubble.playerFired) {
			scriptBubble.speed = new Vector3 (0, 0, 0);
			//Debug.Log(scriptBubble.playerFired);
			/*collider.gameObject.transform.localPosition
					= gameController.moveToCorrectPosition
						(collider.gameObject.transform.localPosition, true);
*/
			//insert bubble
			gameController.insert (collider.gameObject, true);

			gameController.destroyBubbles (collider.gameObject);

			//Detener
			scriptBubble.playerFired = false;
		}
	}

	public void destroy(){
		Destroy (gameObject);
	}

	/**
	 * Implements IBubbleMatrix, because it wants to be in the matrix
	 * 
	 * */
	public Vector3 GetRowCol(){ return rowCol;}
	public void SetRowCol(Vector3 rowColParam){rowCol = rowColParam;}
	
	public Enums.BUBBLECOLOR GetBubbleColor(){return bubbleColor;}
	public void SetBubbleColor(Enums.BUBBLECOLOR bubbleColorParam){bubbleColor = bubbleColorParam;}
}
