﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{

	[Range(0, 1)]
	public float alphaLevel = 0.5f;

	// Use this for initialization
	void Start()
	{
		// Set tilemaps to 50% transparency 
		var tilemaps = GetComponentsInChildren<Tilemap>();
		foreach (var tilemap in tilemaps)
		{
			var bounds = tilemap.GetComponent<TilemapCollider2D>().bounds.size;
			var startingI = (-1 * (int) bounds.x) * 2;
			var startingJ = (-1 * (int) bounds.y) * 2;
			for (int i = startingI; i < ((int) bounds.x) * 2; i++)
			{
				for (int j = startingJ; j < ((int) bounds.x) * 2; j++)
				{
					var location = new Vector3Int(i, j, 0);

					tilemap.RemoveTileFlags(location, TileFlags.LockColor);
					var tileColor = tilemap.GetColor(location);
					var revealedColor = new Color(
						tileColor.r,
						tileColor.g,
						tileColor.b,
						alphaLevel
					);
					tilemap.SetColor(location, revealedColor);
				}
			}

		}
	}
}