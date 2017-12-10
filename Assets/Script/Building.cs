using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Building
{
	public string name;
	public Color color;
	public bool isUnique;
	public int size;
	public Building(string _name, Color _color, bool _isUnique, int _size)
	{
		name = _name;
		color = _color;
		isUnique = _isUnique;
		size = _size;
	}
}
