extends CollisionShape2D

export(bool) var sky = false
export(float) var size = 64.0;

func _ready():
	self.shape.extents = Vector2(get_viewport_rect().size.x, size / 2)
	if sky:
		self.position = Vector2(get_viewport_rect().size.x / 2, size / 2)
	else:
		self.position = Vector2(get_viewport_rect().size.x / 2, get_viewport_rect().size.y - (size / 2))

func _process(delta):
	self.shape.extents = Vector2(get_viewport_rect().size.x, size / 2)
	if sky:
		self.position = Vector2(get_viewport_rect().size.x / 2, size / 2)
	else:
		self.position = Vector2(get_viewport_rect().size.x / 2, get_viewport_rect().size.y - (size / 2))
