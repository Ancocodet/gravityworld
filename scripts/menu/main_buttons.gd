extends HBoxContainer

signal show_leaderboard
signal exit_game
signal toggle_settings

func _on_settings_button_pressed():
	emit_signal("toggle_settings")

func _on_exit_button_pressed():
	emit_signal("exit_game")

func _on_leaderboard_button_pressed():
	emit_signal("show_leaderboard")
