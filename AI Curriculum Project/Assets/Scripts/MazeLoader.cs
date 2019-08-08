﻿using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour
{
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public float size = 2f;

	private MazeCell[,] mazeCells;

	// Use this for initialization
	void Start ()
    {
		InitializeMaze();

        HuntAndKillMazeAlgorithm maze = new HuntAndKillMazeAlgorithm (mazeCells);
		maze.HuntAndKill();
	}
	
	private void InitializeMaze()
    {
		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++)
        {
			for (int c = 0; c < mazeColumns; c++)
            {
				mazeCells [r, c] = new MazeCell ();

				if (c == 0)
                {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
				}

				mazeCells[r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;

				if (r == 0)
                {
					mazeCells[r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells[r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells[r, c].southWall.transform.Rotate (Vector3.up * 90f);
			}
		}
	}
}
