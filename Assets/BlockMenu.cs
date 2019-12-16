using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockMenu : MonoBehaviour
{
	public Text text;

	private BuildController builder;

	void Start()
	{
		builder = GameObject.Find("Builder").GetComponent<BuildController>();
	}

	public void SetLength(Slider length)
	{
		text.text = "Length: " + length.value.ToString();
		builder.SetLength((int)length.value);
		builder.SelectBlock();
	}

	public void SetWidth(Slider width)
	{
		text.text = "Width: " + width.value.ToString();
		builder.SetWidth((int)width.value);
		builder.SelectBlock();
	}

	public void SetSize(Toggle isSmall)
	{
		builder.SetIsSmall(isSmall.isOn);
		builder.SelectBlock();
	}

	public void SetColor(Dropdown color)
	{
		Color[] c = { Color.red, Color.blue, Color.green, Color.gray,
			Color.cyan, Color.white, Color.yellow, Color.magenta };
		builder.SelectColor(c[(int)color.value]);
	}
}
