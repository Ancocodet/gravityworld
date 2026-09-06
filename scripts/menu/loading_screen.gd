extends Control

var tween: Tween
var last_anim: Tweener
var last_progress: float

func _ready():
	$LoadingScreenContainer/ProgressBar.value = 0.0
	visible = true

func finished_loading():
	if last_anim != null:
		$LoadingScreenContainer/ProgressBar.value = 100.0
		get_tween().stop()

	$Animator.play("fade_out")
	await $Animator.animation_finished
	
	visible = false

func get_tween():
	if tween == null:
		tween = get_tree().create_tween().bind_node(self).set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_OUT)	
	return tween

func update_progress(progress: float, replace: bool = false):
	if last_anim != null:
		$LoadingScreenContainer/ProgressBar.value = last_progress
		get_tween().stop()
		tween = null
	
	var current = $LoadingScreenContainer/ProgressBar.value
	if replace:
		last_progress = progress
		await animate_progress(progress)
	else:
		last_progress = current + progress
		await animate_progress(current + progress)
		
func animate_progress(target: float, duration=1.0):
	last_anim = get_tween().tween_property($LoadingScreenContainer/ProgressBar, "value", target, duration)
	await last_anim.finished
