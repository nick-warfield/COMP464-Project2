using System.Collections; using System.Collections.Generic;
using UnityEngine;

public class LegoStack : MonoBehaviour
{
	private Stack<GameObject> back = new Stack<GameObject>();
	private Stack<GameObject> fwd = new Stack<GameObject>();

	public void Start()
	{
		back.Push(Instantiate(new GameObject("State"), transform));
	}

	public void Undo()
	{
		if (back.Count != 1)
		{
			var temp = back.Pop();
			temp.SetActive(false);
			back.Peek().SetActive(true);
			fwd.Push(temp);
		}
	}

	public void Redo()
	{
		if (fwd.Count != 0)
		{
			var temp = fwd.Pop();
			back.Peek().SetActive(false);
			temp.SetActive(true);
			back.Push(temp);
		}
	}

	public void Delete(GameObject lego)
	{
		var current = back.Pop();
		var old = Instantiate(current, transform);
		old.SetActive(false);
		back.Push(old);

		Destroy(lego);
		back.Push(current);

		while (fwd.Count != 0) { Destroy(fwd.Pop()); }
	}
	public void Add(GameObject lego)
	{
		var temp = Instantiate(back.Peek(), transform);
		back.Peek().SetActive(false);
		lego.transform.parent = temp.transform;
		back.Push(temp);

		while (fwd.Count != 0) { Destroy(fwd.Pop()); }
	}
}
