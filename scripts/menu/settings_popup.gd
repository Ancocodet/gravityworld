extends Control

func sync_switches():
	$CenterContainer/PopupBackground/VBoxContainer/VBoxContainer/Sounds/SoundsSwitch.set_pressed(GameData.should_play_sounds())
	$CenterContainer/PopupBackground/VBoxContainer/VBoxContainer/Effects/EffectSwitch.set_pressed(GameData.should_play_effects())
	
func show_popup():
	$Animator.play("fade_in")
	await $Animator.animation_finished

func hide_popup():
	$Animator.play("fade_out")
	await $Animator.animation_finished

func _on_exit_pressed():
	hide_popup()

func _on_sounds_switch_pressed():
	if GameData.should_play_sounds():
		GameData.set_sounds(0.0)
	else:
		GameData.set_sounds(100.0)


func _on_music_switch_pressed():
	if GameData.should_play_effects():
		GameData.set_effects(0.0)
	else:
		GameData.set_effects(100.0)
