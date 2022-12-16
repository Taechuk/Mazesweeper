using Godot;
using System;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public int speed = 148;
	public int health;
	
	public string path, hp_path, losescreen_path, hud_path, win_path;
	//public string hp_path;
	//public string losescreen_path;
	//public string hud_path;
	//public string win_path;
	//PackedScene explosion;
	
	public Vector2 Velocity = Vector2.Zero;
	
	private AnimatedSprite _animatedSprite;
	private AnimatedSprite _explosion;
	
	public string lastDirection = "down";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		_explosion = GetNode<AnimatedSprite>("Explosion");
		
		_explosion.Visible = false;
		
		//path = "res://explosion.tscn";
		//explosion = ResourceLoader.Load<PackedScene>(path);
		hp_path = "/root/Node2D/Camera2D/GUI/MarginContainer/HBoxContainer/LeftPadding/Life";
		losescreen_path = "/root/Node2D/Camera2D/GUI/LoseScreen";
		hud_path = "/root/Node2D/Camera2D/GUI/MarginContainer";
		win_path = "/root/Node2D/Camera2D/GUI/WinScreen";
		health = 6;
		var hp_bar = GetNode<AnimatedSprite>(hp_path);
			hp_bar.Play(health.ToString()+"hp");
	}
	
	public bool keyCheck(string k)
	{
		return Input.IsActionPressed(k);
	}
	
	public void setAnim()
	{
		if(Velocity.x > 0)
		{
			_animatedSprite.Play("walk_right");
			lastDirection = "right";
		}
		else if(Velocity.x < 0)
		{
			_animatedSprite.Play("walk_left");
			lastDirection = "left";
		}
		else if(Velocity.y > 0)
		{
			_animatedSprite.Play("walk_down");
			lastDirection = "down";
		}
		else if(Velocity.y < 0)
		{
			_animatedSprite.Play("walk_up");
			lastDirection = "up";
		}
		
		if(Velocity.x == 0 && Velocity.y == 0)
		{
			_animatedSprite.Play("stand_"+lastDirection);
		}
	}

	public void GetInput()
	{
		Velocity = new Vector2();
		
		if(keyCheck("ui_right"))
			Velocity.x += 1;
		if(keyCheck("ui_left"))
			Velocity.x -= 1;
		if(keyCheck("ui_down"))
			Velocity.y += 1;
		if(keyCheck("ui_up"))
			Velocity.y -= 1;
			
			Velocity = Velocity.Normalized()*speed;
			
			setAnim();
	}

	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		
		Velocity = MoveAndSlide(Velocity);
		
		if(!_explosion.Playing)
		{
			_explosion.Visible = false;
		}
		
		string mineSource = "/root/Node2D/Background/Mines";
		
		if(Position.y < -20)
			win();
		
		for(int i=0;i<GetSlideCount();i++) 
		{
			var collision = GetSlideCollision(i);
			Vector2 coords = new Vector2(Convert.ToSingle(collision.Position.x*3.625),Convert.ToSingle(collision.Position.y*3.625));
			var cell = GetNode<TileMap>(mineSource).WorldToMap(coords);
			
			try
			{ 
			GetNode<Mines>(mineSource).getCell((int)(cell.x), (int)(cell.y)).changeState();
			GetNode<Mines>(mineSource).updateMap((int)(cell.x), (int)(cell.y));
			
			if(GetNode<Mines>(mineSource).getCell((int)(cell.x), (int)(cell.y)).getMine())
				{
					Vector2 pos = new Vector2(Convert.ToSingle(collision.Position.x),Convert.ToSingle(collision.Position.y));
					blowUp(pos);
				}
				
				
			}
			catch{}
		}
	}
	
	public void win()
	{
		GetNode<MarginContainer>(hud_path).Visible = false;
		GetNode<Control>(win_path).Visible = true;
	}
	
	public void blowUp(Vector2 pos)
	{
		
		
		//Explosion e = explosion.Instance<Explosion>();
		//e.Offset = pos;
		_explosion.Frame = 0;
		_explosion.Play("Explosion");
		_explosion.Visible = true;
			
		health = health - 2;
		setUpHPBar();
		
		if(health <= 0)
		{
			var losescreen = GetNode<Control>(losescreen_path);
			losescreen.Visible = true;
		}
	}
	
	public void setUpHPBar()
		{
			
			var hp_bar = GetNode<AnimatedSprite>(hp_path);
			hp_bar.Play(health.ToString()+"hp");
		}
		
	public void takeHit()
		{
			health--;
			var hp_bar = GetNode<AnimatedSprite>(hp_path);
			hp_bar.Play(health.ToString()+"hp");
			
			if(health <= 0)
			{
				var losescreen = GetNode<Control>(losescreen_path);
				losescreen.Visible = true;
			}
		}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
