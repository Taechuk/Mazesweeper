using Godot;
using System;
using System.Collections.Generic;


public class Mines : TileMap
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	public Cells[,] board;
	
	
	public int height = 22;
	public int width = 27;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var rng = new RandomNumberGenerator();
		rng.Randomize();
		height = 22;
		width = 27;
		board = new Cells[22, 27];
		float density = rng.RandfRange((float)0.12,(float)0.14);
		
		for(int row=0;row<height;row++) //génère le tableau de mine
		{
			for(int col=0;col<width;col++)
			{
				if(col==26&&(row==10||row==9))
				{
				board[row,col] = new Cells(false);
				board[row,col].changeState();
				}
				else
				board[row,col] = new Cells(rng.Randf()<density);
			}
		}
		
		setCellsNeighbors();
		showMap();
	}
	
	public void setCellsNeighbors()
	{
		for(int row=0;row<height;row++) //pour chaque cellule...
		{
			for(int col=0;col<width;col++)
			{
				if(board[row,col].isMine)
				continue;

				List<Cells> neighbors = new List<Cells>();

				for(int i=row-1;i<row+2;i++) //fait une liste des voisins
				{
					for(int j=col-1;j<col+2;j++)
					{
						//if(i==row&&j==row)
						//continue;

						try //try-catch pour s'occuper des extremités (cause une exception si coord du voisin est invalide)
						{
							neighbors.Add(board[i,j]);
						}
						catch //rien a faire lors d'un exception, fait juste continuer
						{

						}
					}
				}

				board[row,col].setNeighbors(neighbors.ToArray());
				
			}
		}
	}
	
	public Cells getCell(int x, int y)
	{
		return board[x,y];
	}
	
	public void showMap()
	{
		for(int row=0;row<height;row++)
		{
			for(int col=0;col<width;col++)
			{
				SetCell(row, col, board[row,col].getState());
			}
		}
	}
	
	public void updateMap(int x, int y)
	{
		SetCell(x,y, -1);
		SetCell(x,y, board[x,y].getState());
	}
	

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
