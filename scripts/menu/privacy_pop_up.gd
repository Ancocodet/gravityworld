extends Control

signal accept_policy

func show_popup():
	$Animator.play("fade_in")
	await $Animator.animation_finished

func hide_popup():
	$Animator.play("fade_out")
	await $Animator.animation_finished

func _on_accept_button_pressed():
	emit_signal("accept_policy")
