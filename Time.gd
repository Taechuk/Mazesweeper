extends Label

var timer = 0

func _ready():
	pass 
	
func _process(delta):
	timer += delta
	text = var2str(timer)
	var secs = fmod(timer,60)
	var mins = fmod(timer,60*60) / 60
	
	var time_passed = "%02d : %02d" % [mins,secs]
	text = time_passed

