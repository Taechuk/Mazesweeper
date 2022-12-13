extends Control



func _ready():
	visible = false
	

func _on_MainMenuButton_pressed():
	get_tree().paused = false
	get_tree().change_scene("res://MainMenu.tscn")


func _on_RetryButton_pressed():
	get_tree().paused = false
	get_tree().reload_current_scene()


func _on_QuitButton_pressed():
	get_tree().paused = false
	get_tree().quit()


func _on_LoseScreen_visibility_changed():
	if visible:
		get_tree().paused = true
