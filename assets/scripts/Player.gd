extends KinematicBody2D

signal move
signal hit

onready var _animate_sprite = $AnimatedSprite
onready var _collision_shape = $CollisionShape2D
onready var _area_shape = $Area2D/AreaShape

export(int) var jump_force = 600;
# warning-ignore:export_hint_type_mistmatch
export(float) var gravity = 1200

var velocity = Vector2()

var jumping = false
var ducking = false

func _ready():
	self.position = Vector2(200, get_viewport_rect().size.y - 64)

# warning-ignore:unused_argument
func _process(delta):
	if jumping or !is_on_ground():
		_animate_sprite.play("jump")
		emit_signal("move")
	elif ducking and is_on_ground():
		_animate_sprite.play("duck")
		emit_signal("move")
	else:
		_animate_sprite.play("walk")

func get_input():
	var switch = Input.is_action_just_pressed("switch_gravity")	
	var up = Input.is_action_pressed("move_up")
	var down = Input.is_action_pressed("move_down")
	
	if !up and !down and ducking:
		ducking = false
	
	if switch and is_on_ground():
		switch_gravity()
	
	if !jumping:
		if (up and is_on_floor()) or (down and is_on_ceiling()):
			switch_gravity()
		elif (up and is_on_ceiling()) or (down and is_on_floor()):
			ducking = true
		

func _physics_process(delta):
	get_input()
	velocity.y += gravity * delta

	maybe_flip()

	if jumping and is_on_ground():
		jumping = false
	velocity = move_and_slide(velocity, Vector2(0, -1))

func switch_gravity():
	jumping = true
	gravity *= -1

func maybe_flip():
	if should_flip_up():
		_animate_sprite.flip_v = true
		_collision_shape.position.y *= -1
		_area_shape.position.y *= -1
	if should_flip_down():
		_animate_sprite.flip_v = false
		_collision_shape.position.y *= -1
		_area_shape.position.y *= -1

func should_flip_up():
	if gravity < 0 and self.position.y < get_viewport_rect().size.y / 2:
		return !_animate_sprite.flip_v
	return false

func should_flip_down():
	if gravity > 0 and self.position.y > get_viewport_rect().size.y / 2:
		return _animate_sprite.flip_v
	return false

func is_on_ground():
	return is_on_floor() or is_on_ceiling()

var spawned = false

# warning-ignore:unused_argument
func _on_obstacle_detected(body):
	if spawned:
		hide()
		emit_signal("hit")
	else:
		spawned = true
