using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : MonoBehaviour
{
	public int currentBuilding = 0;
	public GameObject selectBuildingPanel;
	public Text buildingName;
	public Text buildingSize;
	public Image buildingColor;
	public Text buildingType;

	public Text selectedBuildingName;
	public Text selectedBuildingSize;
	public Image selectedBuildingColor;
	public GameObject selectedBuildingPanel;

	public GameObject buildingSystem;

	void Start ()
	{
	
	}

	void Update ()
	{
	
	}

	public void ShowSelectedBuilding(int selectedType)
	{
		selectBuildingPanel.SetActive(false);
		selectedBuildingName.text = "Name: "+buildingSystem.GetComponent<BuildingSystem>().buildings[selectedType].name;
		selectedBuildingSize.text = "Size: "+buildingSystem.GetComponent<BuildingSystem>().buildings[selectedType].size+"x"+buildingSystem.GetComponent<BuildingSystem>().buildings[selectedType].size;
		selectedBuildingColor.color = buildingSystem.GetComponent<BuildingSystem>().buildings[selectedType].color;
		buildingSystem.GetComponent<BuildingSystem>().runningBuildingIndex = selectedType;
		buildingSystem.GetComponent<BuildingSystem>().currentAction = (int)BuildingSystem.Actions.DIALOG;
		selectedBuildingPanel.SetActive(true);
	}
	
	public void BuildingNav(int i)
	{
		buildingSystem.GetComponent<BuildingSystem>().currentAction = (int)BuildingSystem.Actions.DIALOG;
		currentBuilding+=i;
		if(currentBuilding<0)
		{
			currentBuilding=buildingSystem.GetComponent<BuildingSystem>().buildings.Length-1;
		}
		else if(currentBuilding>buildingSystem.GetComponent<BuildingSystem>().buildings.Length-1)
		{
			currentBuilding = 0;
		}
		buildingName.text = "Name: "+buildingSystem.GetComponent<BuildingSystem>().buildings[currentBuilding].name;
		buildingSize.text = "Size: "+buildingSystem.GetComponent<BuildingSystem>().buildings[currentBuilding].size+"x"+buildingSystem.GetComponent<BuildingSystem>().buildings[currentBuilding].size;
		buildingColor.color = buildingSystem.GetComponent<BuildingSystem>().buildings[currentBuilding].color;
		if(buildingSystem.GetComponent<BuildingSystem>().buildings[currentBuilding].isUnique)
		{
			buildingType.text = "Build Type: Unique";
		}
		else
		{
			buildingType.text = "Build Type: Common";
		}
	}
	public void StartBuilding()
	{
		buildingSystem.GetComponent<BuildingSystem>().StartBuilding(currentBuilding);
	}
	public void Exit()
	{
		Application.Quit();
	}
}
