extends ParallaxBackground

func _ready():
	$Bottom/Sprite.region_rect.size.x = get_viewport().size.x
	$Bottom.position.y = get_viewport().size.y - 64
	
	$Top/Sprite.region_rect.size.x = get_viewport().size.x
	$Top/Sprite.flip_v = true
	$Top.position.y = 0

func _process(delta):
	$Bottom/Sprite.region_rect.size.x = get_viewport().size.x
	$Bottom.position.y = get_viewport().size.y - 64
	
	$Top/Sprite.region_rect.size.x = get_viewport().size.x
	$Top.position.y = 0

func _on_Player_move():
	scroll_base_offset -= Vector2(1, 0)
