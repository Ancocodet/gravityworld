extends Node

signal show_policy_popup
signal hide_policy_popup

var androidUtility

func init():
	if not GameData.is_policy_accepted():
		emit_signal("show_policy_popup")
	else:
		AccountManager.loadAccount()
	
func accept_policy():
	GameData.cache["policy_accepted"] = true
	GameData.save()
	emit_signal("hide_policy_popup")
	
func save_cache(data, path):
	ResourceSaver.save(data, path)
