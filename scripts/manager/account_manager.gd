extends Node

var play_services = null

signal login_succeeded
signal login_failed

signal loading_succeeded
signal loading_failed

func _ready():
	if Engine.has_singleton("GodotGooglePlayServices"):
		play_services = Engine.get_singleton("GodotGooglePlayServices")

func loadAccount():
	if play_services != null:
		if play_services.isAvailable():
			play_services.sign_in_success.connect(_on_login_success)
			play_services.sign_in_failure.connect(_on_login_failure)
			
			play_services.player_load_success.connect(_on_player_load_success)
			play_services.player_load_failure.connect(_on_player_load_failure)
			
			play_services.init()
		else:
			emit_signal("login_failed")
			emit_signal("loading_failed")	
	else:
		emit_signal("login_failed")
		emit_signal("loading_failed")

func is_signed_in():
	if play_services != null:
		return play_services.isSignedIn()
	return false

func get_player_id():
	if is_signed_in():
		if play_services != null:
			return play_services.getPlayerId()
	return null

func get_player_name():
	if is_signed_in():
		if play_services != null:
			return play_services.getPlayerName()
	return "unknown"

func _on_login_success():
	emit_signal("login_succeeded")

func _on_login_failure():
	emit_signal("login_failed")
	
func _on_player_load_success():
	emit_signal("loading_succeeded")

func _on_player_load_failure():
	emit_signal("loading_failed")
