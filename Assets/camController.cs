using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour
{
	public Vector3 center = Vector3.zero;

    void Update()
    {
		if (Input.GetKey(KeyCode.A))
		{
			transform.RotateAround(center, Vector3.up, 150 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.RotateAround(center, Vector3.up, -150 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.W))
		{
				transform.Translate(Vector3.forward * 20 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.back * 20 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.E))
		{
			transform.Translate(Vector3.up * 10 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.Q))
		{
			transform.Translate(Vector3.down * 10 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }
        
    }
}
