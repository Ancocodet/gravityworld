extends Node2D

var score : int = 0
var void_meter : float = 0.0

var scoring : float = 0
var speed : float = 0.25

var countdown : int = 5
var running : bool = false

func _ready():
	while countdown > -1:
		$Countdown.text = str(countdown)
		countdown -= 1
		await get_tree().create_timer(1.0).timeout
	
	$Countdown.visible = false
	running = true

func _process(_delta):
	if running:
		speed += 0.001
		
		scoring += speed
		if scoring == 1:
			score += 1
	
