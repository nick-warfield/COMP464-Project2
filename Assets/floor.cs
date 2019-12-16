using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor : MonoBehaviour
{
	public int Size = 10;

    void Start()
    {
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		Color color = randColor();
		Size = GameObject.FindWithTag("Settings").GetComponent<gameSettings>().BoardSize;

		for (int i = 0; i < Size; ++i)
		{
			for (int j = 0; j < Size; ++j)
			{
				var child = Instantiate(
						Resources.Load<GameObject>("Legos/Reg 1x1"),
						transform);
				child.transform.localPosition = new Vector3(i, 0, j);
				foreach (Renderer ren in child.GetComponentsInChildren<Renderer>())
				{
					ren.material.color = color;
				}
				foreach (BoxCollider box in child.GetComponentsInChildren<BoxCollider>())
				{
					box.gameObject.layer = 8;
					box.tag = "Floor";
				}
			}
		}

		transform.position = new Vector3(-Size / 2, 0, -Size / 2);
    }
	
	Color randColor()
	{
		Color[] c = { Color.red, Color.blue, Color.green, Color.gray,
			Color.cyan, Color.white, Color.yellow, Color.magenta };
		return c[Random.Range(0, c.Length)];
	}
}
