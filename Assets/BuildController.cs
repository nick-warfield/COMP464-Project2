using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildController : MonoBehaviour
{
	public Material BlockHover;
	public Material Opaque;
	public LegoStack legoStack;

	private GameObject lego;
	private Color legoColor = Color.red;
	private Vector3 legoPosition = new Vector3(0, 1, 0);
	private Camera cam;
	private int m_length = 1, m_width = 2;
	private bool m_isSmall = false;

	void Start()
	{
		cam = Camera.main;
		SelectBlock();
		SelectColor(Color.red);
	}

	void Update()
	{
		// translation
		// mouse position
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		bool rayhit = Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 8));
		if (rayhit)
		{
			transform.position = new Vector3(
					Mathf.Round(hit.point.x),
					Mathf.Ceil(hit.point.y),
					Mathf.Round(hit.point.z));
		}

		// rotation
		if (Input.GetKeyDown(KeyCode.Space))
		{
			 transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + 
					new Vector3(0, 90 * (isShiftPressed() ? -1 : 1), 0));
		}

		if (rayhit && !EventSystem.current.IsPointerOverGameObject())
		{
			if (Input.GetMouseButtonDown(0))
			{
				placeLego();
			}
			if (Input.GetMouseButtonDown(1)
					&& hit.collider.tag != "Floor")
			{
				legoStack.Delete(hit.transform.parent.gameObject);
			}
		}

	}

	public void SetLength(int length) { m_length = length; }
	public void SetWidth(int width) { m_width = width; }
	public void SetIsSmall(bool isSmall) { m_isSmall = isSmall; }

	public void SelectBlock()
	{
		Destroy(lego);
		lego = Instantiate(Resources.Load<GameObject>(
				"Legos/"
				+ (m_isSmall ? "Small " : "Reg ")
				+ m_length.ToString()
				+ "x"
				+ m_width.ToString()),
				transform);
		foreach (Renderer ren in GetComponentsInChildren<Renderer>())
		{
			ren.material = BlockHover;
		}
	}

	public void SelectColor(Color color)
	{
		legoColor = color;
	}

	void placeLego()
	{
		var block = Instantiate(lego, transform.position, transform.rotation);
		foreach (Renderer ren in block.GetComponentsInChildren<Renderer>())
		{
			ren.material = Opaque;
			ren.material.color = legoColor;
		}
		foreach (BoxCollider box in block.GetComponentsInChildren<BoxCollider>())
		{
			box.gameObject.layer = 8;
		}
		legoStack.Add(block);
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

	bool isShiftPressed()
	{
		return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
	}
}
