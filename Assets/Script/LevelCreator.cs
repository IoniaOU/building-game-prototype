using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour
{
	public int mapWitdh;
	public int mapLength;
	public Material tileMat;
	void Start ()
	{
		for(int i = 0; i<mapLength+2;i++)
		{
			for(int j = 0; j<mapWitdh+2; j++)
			{
				if((i==0)||(j==0)||(i==mapLength+1)||(j==mapWitdh+1))
				{
					BuildFrame(i,j);
				}
				else
				{
					GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
					plane.name = i+"."+j;
					plane.tag = "Tile";
					plane.transform.position = new Vector3(i*10,0,j*10);
					plane.GetComponent<Renderer>().material = tileMat;
					plane.layer = 8;
				}
			}
		}
	}
	private void BuildFrame(float x, float z)
	{
		GameObject frameCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		frameCube.tag = "Building";
		frameCube.transform.position = new Vector3(x*10,0,z*10);
		Destroy(frameCube.GetComponent<Renderer>());
	}
}
