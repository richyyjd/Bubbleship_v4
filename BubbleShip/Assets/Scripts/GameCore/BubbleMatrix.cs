﻿using UnityEngine;
using System.Collections;

public class BubbleMatrix
{

	public const float ROW_SIZE = 1.4f;
	public const float COL_SIZE = 1.4f;
	public const float DIFERENCE = 2f;

	Hashtable matrixBubble;

	public BubbleMatrix(){
		matrixBubble = new Hashtable();
	}


	//Move Bubble to correct position
	public Vector3 moveToCorrectPosition (Vector3 position, Vector3 rowCol)
	{

		float x, y;
		//Debug.Log (rowCol.y % 2);
		if (rowCol.y % 2 == 0) {
			x = rowCol.x * COL_SIZE + (COL_SIZE / DIFERENCE) + (COL_SIZE / DIFERENCE);
		} else {
			x = rowCol.x * COL_SIZE + (COL_SIZE / DIFERENCE);
		}
		y = rowCol.y * ROW_SIZE + (ROW_SIZE / DIFERENCE);

		return new Vector3 (x, y, position.z);
	}

	//Calculate col and row with x and y position
	public Vector3 calcColAndRow (Vector3 position)
	{
		int col = (int)(position.x / COL_SIZE);
		int row = (int)(position.y / ROW_SIZE);

		return new Vector3 (col, row, position.z);
	}

	//insert Bubble into matrix
	public void insert (GameObject bubbleObj, bool substract)
	{
		//calcColAndRow and insert into matrix
		Vector3 rowCol = calcColAndRow (bubbleObj.transform.localPosition);
		if (substract) {
			if (rowCol.y % 2 == 0){
				bubbleObj.transform.localPosition -= new Vector3(COL_SIZE / DIFERENCE, 0, 0);
				rowCol = calcColAndRow (bubbleObj.transform.localPosition);
			}
		}

		IBubbleMatrix bubbleScript = bubbleObj.GetComponent<IBubbleMatrix> ();
		bubbleScript.SetRowCol(rowCol);
		if (!matrixBubble.ContainsKey ("x:" + rowCol.x + ", y:" + rowCol.y)) {
			matrixBubble.Add ("x:" + rowCol.x + ", y:" + rowCol.y, bubbleObj);
		}
		bubbleObj.transform.localPosition = 
			moveToCorrectPosition (bubbleObj.transform.localPosition, bubbleScript.GetRowCol());
		/*Debug.Log ("--MATRIX--"+rowCol);
		foreach(string key in matrixBubble.Keys)
			Debug.Log (key+"__"+rowCol);
		Debug.Log ("--END_MATRIX--"+rowCol);*/
	}

	public void remove(string key){
		matrixBubble.Remove (key);
	}

	public GameObject[] getNeighbours(IBubbleMatrix bubbleScript, Vector3 localPosition){
		Vector3 rowCol = bubbleScript.GetRowCol();
		return getNeighbours (rowCol);
	}

	public GameObject[] getNeighbours(Vector3 rowCol){
		//Debug.Log ("--x:"+rowCol.x+", y:"+rowCol.y);
		GameObject[] neighbours = new GameObject[6];
		//Left and Right
		neighbours[0] = matrixBubble["x:"+(rowCol.x-1)+", y:"+(rowCol.y)] as GameObject;
		neighbours[1] = matrixBubble["x:"+(rowCol.x+1)+", y:"+(rowCol.y)] as GameObject;
		//Up and Down
		neighbours[2] = matrixBubble["x:"+(rowCol.x)+", y:"+(rowCol.y-1)] as GameObject;
		neighbours[3] = matrixBubble["x:"+(rowCol.x)+", y:"+(rowCol.y+1)] as GameObject;
		//Debug.Log ("--------"+rowCol+"------");
		//Debug.Log ("x:"+(rowCol.x-1)+", y:"+(rowCol.y)+"_"+neighbours[0]+"-0");
		//Debug.Log ("x:"+(rowCol.x+1)+", y:"+(rowCol.y)+"_"+neighbours[1]+"-1");
		//Debug.Log ("x:"+(rowCol.x)+", y:"+(rowCol.y-1)+"_"+neighbours[2]+"-2");
		//Debug.Log ("x:"+(rowCol.x)+", y:"+(rowCol.y+1)+"_"+neighbours[3]+"-3");
		if (rowCol.y % 2 == 0) {
			//In a even row we see left
			neighbours[4] = matrixBubble["x:"+(rowCol.x+1)+", y:"+(rowCol.y+1)] as GameObject;
			neighbours[5] = matrixBubble["x:"+(rowCol.x+1)+", y:"+(rowCol.y-1)] as GameObject;
			//Debug.Log ("x:"+(rowCol.x+1)+", y:"+(rowCol.y+1)+"_"+neighbours[4]+"-4");
			//Debug.Log ("x:"+(rowCol.x+1)+", y:"+(rowCol.y-1)+"_"+neighbours[5]+"-5");
		} else {
			//In a odd row we see right
			neighbours [4] = matrixBubble ["x:" + (rowCol.x - 1) + ", y:" + (rowCol.y + 1)] as GameObject;
			neighbours [5] = matrixBubble ["x:" + (rowCol.x - 1) + ", y:" + (rowCol.y - 1)] as GameObject;
			//Debug.Log ("x:" + (rowCol.x - 1) + ", y:" + (rowCol.y + 1)+"_"+neighbours[4]+"-4");
			//Debug.Log ("x:" + (rowCol.x - 1) + ", y:" + (rowCol.y - 1)+"_"+neighbours[5]+"-5");
		}
		
		//Debug.Log ("--------------------");
		//Debug.Log ("--------------------");
		return neighbours;
	}

	//Richard
	public GameObject[] getNeighbours_forBomb(IBubbleMatrix bubbleScript, Vector3 localPosition){
		Vector3 rowCol = bubbleScript.GetRowCol();
		//Debug.Log ("--x:"+rowCol.x+", y:"+rowCol.y);
		GameObject[] neighbours = new GameObject[6];
		//Left and Right
		neighbours[0] = matrixBubble["x:"+(rowCol.x-1)+", y:"+(rowCol.y)] as GameObject;
		//neighbours[0] = matrixBubble["x:"+(rowCol.x-2)+", y:"+(rowCol.y)] as GameObject;

		neighbours[1] = matrixBubble["x:"+(rowCol.x+1)+", y:"+(rowCol.y)] as GameObject;
		//neighbours[1] = matrixBubble["x:"+(rowCol.x+2)+", y:"+(rowCol.y)] as GameObject;

		//Up and Down
		neighbours[2] = matrixBubble["x:"+(rowCol.x)+", y:"+(rowCol.y-1)] as GameObject;
		//neighbours[2] = matrixBubble["x:"+(rowCol.x)+", y:"+(rowCol.y-2)] as GameObject;

		neighbours[3] = matrixBubble["x:"+(rowCol.x)+", y:"+(rowCol.y+1)] as GameObject;
		//neighbours[3] = matrixBubble["x:"+(rowCol.x)+", y:"+(rowCol.y+2)] as GameObject;

		if (rowCol.y % 2 == 0) {
			//In a even row we see left
			neighbours[4] = matrixBubble["x:"+(rowCol.x+1)+", y:"+(rowCol.y+1)] as GameObject;
			neighbours[5] = matrixBubble["x:"+(rowCol.x+1)+", y:"+(rowCol.y-1)] as GameObject;
		} else {
			//In a odd row we see right
			neighbours [4] = matrixBubble ["x:" + (rowCol.x - 1) + ", y:" + (rowCol.y + 1)] as GameObject;
			neighbours [5] = matrixBubble ["x:" + (rowCol.x - 1) + ", y:" + (rowCol.y - 1)] as GameObject;
		}
		//Debug.Log (rowCol+""+neighbours[0]+"0");
		//Debug.Log (rowCol+""+neighbours[1]+"1");
		//Debug.Log (rowCol+""+neighbours[2]+"2");
		//Debug.Log (rowCol+""+neighbours[3]+"3");
		//Debug.Log (rowCol+""+neighbours[4]+"4");
		//Debug.Log (rowCol+""+neighbours[5]+"5");
		return neighbours;
	}


}
