using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuSystem : MonoBehaviour
{
	public Button continueButton;

	void Start()
	{
		if(PlayerPrefs.HasKey("building0"))
		{
			continueButton.interactable = true;
		}
	}

	public void StartGame(bool isNewGame)
	{
		if(isNewGame)
		{
			PlayerPrefs.DeleteAll();
		}
		Application.LoadLevel(1);
	}
}
