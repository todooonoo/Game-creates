using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpeedup : MonoBehaviour
{
	 [SerializeField]
	 private float speed = 3.0f;

	 void Update()
	 {
		  if (Input.GetKeyDown(KeyCode.Space))
		  {
				Time.timeScale = speed;
		  }
		  else if (Input.GetKeyUp(KeyCode.Space))
		  {
				Time.timeScale = 1.0f;
		  }
	 }
}
