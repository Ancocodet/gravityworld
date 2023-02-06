extends KinematicBody2D

func _ready():
	position.x = -125
	position.y = get_viewport_rect().size.y / 2
	$Collider.shape.extents = Vector2(125, get_viewport_rect().size.y)
	$Sprite.region_rect.size.x = 250
	$Sprite.region_rect.size.y = get_viewport_rect().size.y

func _process(delta):
	position.y = get_viewport_rect().size.y / 2
	position.x += 0.05
	$Sprite.region_rect.size.y = get_viewport_rect().size.y

func _on_Player_move():
	position.x = max(-125, position.x - 0.5)
