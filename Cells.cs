using Godot;
using System;
using System.Collections.Generic;

public class Cells
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public bool isMine;
	public Cells[] neighbors;
	public int numBombs;
	public bool isRevealed;
	
	public Cells(bool mine)
	{
		isMine = mine;
		isRevealed = false;
		numBombs = 0;
	}
	
	
	public Cells[] getNeighbors()
	{
		return neighbors;
	}
	
	public void setNeighbors(Cells[] neighbors)
	{
		this.neighbors = neighbors;
		setNumBombs();
	}
	
	public void setNumBombs()
	{
		for(int i=0; i<neighbors.Length; i++)
		{
			if(neighbors[i].getMine())
			numBombs++;
		}
		int check = 0;
		for(int i=0; i<neighbors.Length; i++)
		{
			if(neighbors[i].getMine())
			check++;
		}
		
		if(numBombs!=check)
		{
			numBombs = check;
		}
	}
	
	public void changeState()
	{
		isRevealed = true;
	}
	
	public bool getMine()
	{
		return isMine;
	}
	
	public int getState() //mauvais indexage des tiles, donc les nombres ne font pas une tonne de sens.
	{
		if(isRevealed)
		{
			if(isMine)
			{
				return 12; //return mine
			}
			
			
			return numBombs + 1; //return numBomb, tile 0 est a l'id 1, et les autres suivent... c'est tout comme lua
		}
		return 10; //return facedown
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
