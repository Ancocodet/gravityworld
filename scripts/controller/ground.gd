extends Node

@export var texture : Texture2D = null
@export var texture_size : int = 256
@export var top : bool = false

var elements : int
var rects : Array
var maxX: float

func _ready():
	var width = get_viewport().get_visible_rect().size.x
	var height = get_viewport().get_visible_rect().size.y
	elements = int(width / texture_size) * 3
	
	rects = Array()
	maxX = (elements - 1) * texture_size * 0.5
	
	for i in range(0, elements, 1):
		var rect = TextureRect.new()
		
		rect.texture = texture
		rect.flip_v = top
		
		if top:
			rect.position = Vector2(i * texture_size * 0.5, 0)
		else:
			rect.position = Vector2(i * texture_size * 0.5, (height - int(texture_size / 2.0)))
		
		rect.scale = Vector2(0.5, 0.5)
		
		rects.append(rect)
		self.add_child(rect)

func move(speed):
	maxX -= speed
	for rect in rects:
		rect.position.x -= speed
	maybe_move_to_end()

func maybe_move_to_end():
	var out_of_bounds = rects.filter(filter_elements)
	if out_of_bounds.size() > 0:
		for rect in out_of_bounds:
			rect.position.x = maxX + (texture_size * 0.5)
			maxX = rect.position.x
			
func sort_elements(a, b):
	if a.position.x > b.position.x:
		return 1
	elif b.position.x > a.position.x:
		return -1
	return 0

func filter_elements(element):
	return element.position.x + (texture_size * 0.5) < 0
