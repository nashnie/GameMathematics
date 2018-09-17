using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public Point pt;
	public Line line;


	// Use this for initialization
	void Start () {
		float distance = GameMathematicsStatics.CalculatePointAndLineDistance (pt, line);
		Debug.Log ("CalculatePointAndLineDistance distance " + distance);
	}
}
