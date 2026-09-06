extends Control

var current_step: int = 0
var loading_steps: int = 3

func _ready():
	GameManager.show_policy_popup.connect(_on_show_policy)
	GameManager.hide_policy_popup.connect(_on_hide_policy)
	
	AccountManager.login_succeeded.connect(_on_login_succeeded)
	AccountManager.login_failed.connect(_on_login_failed)
	
	AccountManager.loading_succeeded.connect(_on_loading_finished)
	AccountManager.loading_failed.connect(_on_loading_finished)
	
	GameData.load_data()
	if GameData.is_policy_accepted():
		loading_steps = 2
		
	GameManager.init()
	await $SettingsPopUp.sync_switches()

func _notification(what):
	if what == NOTIFICATION_WM_CLOSE_REQUEST:
		get_tree().quit()

func _on_show_policy():
	await $PrivacyPopUp.show_popup()
	
func _on_hide_policy():
	await $PrivacyPopUp.hide_popup()
	current_step += 1
	await $LoadingScreen.update_progress((float(current_step)/ float(loading_steps)) * 100.0, true)
	AccountManager.loadAccount()

func hide_loading_screen():
	await $LoadingScreen.finished_loading()

func _on_policy_accepted():
	GameManager.accept_policy()
	await get_tree().create_timer(0.5).timeout

func _on_login_succeeded():
	$MarginContainer/HBoxContainer/LeaderboardButton.disabled = false
	current_step += 1
	await $LoadingScreen.update_progress((float(current_step)/ float(loading_steps)) * 100.0, true)
	
func _on_login_failed():
	current_step += 2
	await $LoadingScreen.update_progress((float(current_step)/ float(loading_steps)) * 100.0, true)
	await get_tree().create_timer(0.15).timeout
	hide_loading_screen()
	
func _on_loading_finished():
	current_step += 1
	await $LoadingScreen.update_progress((float(current_step)/ float(loading_steps)) * 100.0, true)
	await get_tree().create_timer(0.15).timeout
	hide_loading_screen()

func _switch_mute():
	if GameData.is_muted():
		$MenuContainer/ButtonCnotainer/SettingsOverlay/SettingsContainer/SoundButtonUnmuted.visible = true
		$MenuContainer/ButtonCnotainer/SettingsOverlay/SettingsContainer/SoundButtonMuted.visible = false
		GameData.set_muted(100.0)
	else:
		$MenuContainer/ButtonCnotainer/SettingsOverlay/SettingsContainer/SoundButtonUnmuted.visible = false
		$MenuContainer/ButtonCnotainer/SettingsOverlay/SettingsContainer/SoundButtonMuted.visible = true
		GameData.set_muted(0.0)

func _on_play_button_pressed():
	SceneManager.goto_scene("res://scenes/ingame.tscn")

func _on_buttons_toggle_settings():
	await $SettingsPopUp.show_popup()
