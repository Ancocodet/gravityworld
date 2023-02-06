extends Node

export(Array, PackedScene) var mob_scene : Array

var obstacles : Array
var score

var void_size : float

func _ready():
	void_size = 0
	randomize()
	$ObstacleTimer.start()

func _on_ObstacleTimer_timeout():
	spawn_obstalce()
	
func spawn_obstalce():
	if mob_scene.size() <= 0:
		return
	
	var obstacle = mob_scene[randi() % mob_scene.size()].instance()
	
	var spawn_location = $Spawner
	obstacle.position = spawn_location.position
	
	add_child(obstacle)
	obstacles.append(obstacle)

func _on_Player_hit():
	print_debug("Player died")

func _on_Player_move():
	score += 1
