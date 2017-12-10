using UnityEngine;
using System.Collections;

public class BuildingSystem : MonoBehaviour
{
	public Building[] buildings;
	public GameObject currentBuilding;
	public int currentBuildingIndex;
	public int currentAction;

	public GameObject levelCreator;
	public GameObject uiSystem;

	public int runningBuildingIndex;

	public enum Actions{NOTBUILDING, BUILDING, DIALOG};

	public delegate void Del(string message);

	void Start ()
	{
		currentAction = (int)Actions.NOTBUILDING;
		int i=0;
		while(PlayerPrefs.HasKey("building"+i))
		{
			GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
			float size = ((buildings[PlayerPrefs.GetInt("building"+i)].size-1)*10)+8;
			building.transform.position = new Vector3(PlayerPrefs.GetFloat("buildingX"+i),0,PlayerPrefs.GetFloat("buildingZ"+i));
			building.transform.localScale = new Vector3(size,size,size);
			building.GetComponent<Renderer>().material.color = buildings[PlayerPrefs.GetInt("building"+i)].color;
			building.tag = "Building";
			building.AddComponent<BuildingControl>();
			building.GetComponent<BuildingControl>().buildingType = PlayerPrefs.GetInt("building"+i);
			i++;
		}
	}

	void Update ()
	{
		if(currentAction==(int)Actions.BUILDING)
		{
			RaycastHit vHit = new RaycastHit();
			Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			LayerMask lm = 1<<8;
			if(Physics.Raycast(vRay, out vHit,1000,lm)) 
			{
				float x,z;
				if(buildings[currentBuildingIndex].size%2==1)
				{
					if(vHit.point.x>5.0f)
					{
						x = vHit.point.x-(vHit.point.x%10)+10;
					}
					else
					{
						x = vHit.point.x-(vHit.point.x%10);
					}

					if(vHit.point.z>5.0f)
					{
						z = vHit.point.z-(vHit.point.z%10)+10;
					}
					else
					{
						z = vHit.point.z-(vHit.point.z%10);
					}
				}
				else
				{
					if(vHit.point.x>5.0f)
					{
						x = vHit.point.x-(vHit.point.x%10)+5;
					}
					else
					{
						x = vHit.point.x-(vHit.point.x%10)-5;
					}
					
					if(vHit.point.z>5.0f)
					{
						z = vHit.point.z-(vHit.point.z%10)+5;
					}
					else
					{
						z = vHit.point.z-(vHit.point.z%10)-5;
					}
				}
				currentBuilding.transform.position = new Vector3(x,0,z);

			}
			if(Input.GetButtonDown("Fire1"))
			{
				if(currentBuilding.GetComponent<BuildingControl>().buildPermission)
				{
					currentAction=(int)Actions.NOTBUILDING;
					currentBuilding.GetComponent<BoxCollider>().isTrigger=false;
					int i = -1;
					do
					{
						i++;
					}
					while(PlayerPrefs.HasKey("building"+i));
					PlayerPrefs.SetInt("building"+i,currentBuildingIndex);
					PlayerPrefs.SetFloat("buildingX"+i,currentBuilding.transform.position.x);
					PlayerPrefs.SetFloat("buildingZ"+i,currentBuilding.transform.position.z);
				}
			}
		}
		else if(currentAction==(int)Actions.NOTBUILDING)
		{
			RaycastHit vHit = new RaycastHit();
			Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(vRay, out vHit, 1000)) 
			{
				if(vHit.collider.tag=="Building")
				{
					vHit.collider.GetComponent<BuildingControl>().onFocus = true;
					if(Input.GetButtonDown("Fire1"))
					{
						uiSystem.GetComponent<UISystem>().ShowSelectedBuilding(vHit.collider.GetComponent<BuildingControl>().buildingType);
					}
				}
			}
		}

	}

	public void StartBuilding(int selectedBuilding)
	{
		currentBuildingIndex = selectedBuilding;
		GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
		float size = ((buildings[selectedBuilding].size-1)*10)+8;
		building.transform.localScale = new Vector3(size,size,size);
		building.GetComponent<BoxCollider>().isTrigger = true;

		//OnTrigger... functions doesn't work without rigidbody.
		building.AddComponent<Rigidbody>();
		building.GetComponent<Rigidbody>().isKinematic=true;

		building.GetComponent<Renderer>().material.color = buildings[selectedBuilding].color;
		building.tag = "Building";
		building.AddComponent<BuildingControl>();
		building.GetComponent<BuildingControl>().buildingType = selectedBuilding;
		currentBuilding = building;
		currentAction = (int)BuildingSystem.Actions.BUILDING;
	}

	public void RunSelectedBuilding()
	{
		if(buildings[runningBuildingIndex].isUnique)
		{
			//TODO:go to this building's unique function
			print (buildings[runningBuildingIndex].name+"'s unique function will run");
		}
		else
		{
			//TODO:go to common function
			print("Common function will run");
		}
	}

	public void CloseDialog()
	{
		currentAction=(int)Actions.NOTBUILDING;
	}
}
