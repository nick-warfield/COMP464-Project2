using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	private Camera cam;

	public float Speed;
	private int bounds;

	void Start()
	{
		bounds = 5;
		cam = Camera.main;
		var gm = GameObject.FindWithTag("Settings");
		if (gm != null)
		{
			var settings = gm.GetComponent<gameSettings>();
			bounds = settings.BoardSize / 2;
			Speed = settings.FallSpeed;
		}
	}

	void Update()
	{
		// translation
		Vector3 dir = Vector3.zero;
		if (Input.GetKeyDown(KeyCode.LeftArrow)) { dir = Vector3.left; }
		if (Input.GetKeyDown(KeyCode.RightArrow)) { dir  = Vector3.right; }
		if (Input.GetKeyDown(KeyCode.UpArrow)) { dir = Vector3.forward; }
		if (Input.GetKeyDown(KeyCode.DownArrow)) { dir = Vector3.back; }
		dir = snapVector(dir);

		// rotation
		if (Input.GetKeyDown(KeyCode.Space) && dir == Vector3.zero)
		{
			 transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + 
					new Vector3(0, 90 * (isShiftPressed() ? -1 : 1), 0));
		}

		transform.position = transform.position + dir +
			Vector3.down * Speed * Time.deltaTime;

		boundsFix();
		if (transform.childCount == 0) { spawnLego(); }
		Debug.DrawRay(
				transform.position + new Vector3(0, 100, 0),
				Vector3.down * 1000,
				Color.red);
	}

	void OnCollisionEnter(Collision collision)
	{
		transform.position = new Vector3(
				Mathf.Round(transform.position.x),
				Mathf.Round(transform.position.y),
				Mathf.Round(transform.position.z));
		foreach(var box in GetComponentsInChildren<BoxCollider>())
		{
			box.size = Vector3.one;
		}
		transform.DetachChildren();
	}

	// snaps vector, it works but there's probably some better way to do this
	Vector3 snapVector(Vector3 dir)
	{
		if (dir == Vector3.zero) { return Vector3.zero; }
		float xdot = Vector3.Dot(dir, cam.transform.right);
		float zdot = Vector3.Dot(dir, cam.transform.forward);
		xdot *= (dir == Vector3.right || dir == Vector3.left ? 1 : -1);
		zdot *= (dir == Vector3.forward || dir == Vector3.back ? 1 : -1);

		if (Mathf.Abs(xdot) > Mathf.Abs(zdot))
		{
			return xdot > 0 ? Vector3.right : Vector3.left;
		}
		else
		{
			return zdot > 0 ? Vector3.forward : Vector3.back;
		}
	}

	void spawnLego()
	{
		RaycastHit hit;
		if (Physics.Raycast(
					new Vector3(
						Random.Range(-bounds, bounds),
						100,
						Random.Range(-bounds, bounds)),
					Vector3.down,
					out hit))
		{
			transform.position = hit.point + new Vector3(0, 20, 0);
		}
		else
		{
			transform.position = new Vector3(0, 20, 0);
		}

		var block = Instantiate(
				Resources.Load<GameObject>(
					"Legos/"
					+ (0 == Random.Range(0, 2) ? "Reg " : "Small ")
					+ Random.Range(1, 3).ToString()
					+ "x"
					+ Random.Range(1, 7).ToString()),
				transform);
		block.transform.localPosition = Vector3.zero;
		
		Color c = randColor();
		foreach (Renderer ren in block.GetComponentsInChildren<Renderer>())
		{
			ren.material.color = c;
		}
		block.GetComponentsInChildren<BoxCollider>()[0].size
			= new Vector3(0.9f, 1, 0.9f);
	}

	void boundsFix()
	{
		float x = transform.position.x, z = transform.position.z;
		if (x >= bounds) { x = bounds - 1; }
		else if (x < -bounds) { x = -bounds; }
		if (z >= bounds) { z = bounds - 1; }
		else if (z < -bounds) { z = -bounds; }
		transform.position = new Vector3(x, transform.position.y, z);
	}

	Color randColor()
	{
		Color[] c = { Color.red, Color.blue, Color.green, Color.gray,
			Color.cyan, Color.white, Color.yellow, Color.magenta };
		return c[Random.Range(0, c.Length)];
	}

	bool isShiftPressed()
	{
		return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
	}
}
