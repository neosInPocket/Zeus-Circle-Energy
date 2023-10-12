using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
	[SerializeField] private MainMenuController _mainMenuController;
 	void Start()
	{
		_mainMenuController.Initialize();
	}
}
