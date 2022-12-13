extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	visible = false


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_WinScreen_visibility_changed():
	if visible:
		#var time = get_tree().get_root().get_node("Camera2D/GUI/MarginContainer/HBoxContainer/VBoxContainer/Time")
		#var newtime = get_node("ColorRect/TimeTotal")
		#newtime.text = time.text
		get_tree().paused = true


func _on_ContinueButton_pressed():
	get_tree().paused = false
	get_tree().reload_current_scene()


func _on_QuitRunButton_pressed():
	get_tree().paused = false
	get_tree().change_scene("res://MainMenu.tscn")
