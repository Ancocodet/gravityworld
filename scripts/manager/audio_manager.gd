extends Node
class_name AudioManager

func _ready():
	process_mode = Node.PROCESS_MODE_ALWAYS


func play_sound(bus: String, audio: AudioStream, volume_db: float = 0) -> AudioStreamPlayer2D:
	var player = AudioStreamPlayer2D.new()
	
	player.volume_db = volume_db
	player.stream = audio
	
	var bus_index = AudioServer.get_bus_index(bus)
	if bus_index != -1:
		player.bus = bus
	else:
		return null
		
	add_child(player)
	player.play(0.0)
	
	return player
