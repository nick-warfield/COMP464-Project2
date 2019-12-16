using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class gameSettings : MonoBehaviour
{
	public InputField sizeText, speedText;
	public Toggle mode;

	public int BoardSize = 5;
	public float FallSpeed = 50;
	public bool isBreakMode = false;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void SetSize() { BoardSize = int.Parse(sizeText.text); }
	public void SetSpeed() { FallSpeed = int.Parse(speedText.text); }
	public void SetMode() { isBreakMode = mode.isOn; }
}
