extends Node

const FILE_NAME = "user://game-data.json"
const ENCRYPTION_PASS = "Mwt3BGuK-ou4nF4YC4uwiPu4FzuK24"

const POLICY_VERSION = 0.1

var cache = {
	"policy_accepted": false,
	"highscore": 0.0,
	"settings": {
		"sound": 100.0,
		"effects": 100.0
	}
}

func is_policy_accepted():
	return cache["policy_accepted"]
	
func set_sounds(volume: float):
	cache["settings"]["sound"] = volume
	save()

func set_effects(volume: float):
	cache["settings"]["effects"] = volume
	save()	

func should_play_sounds():
	return cache["settings"]["sound"] > 0.0
	
func should_play_effects():
	return cache["settings"]["effects"] > 0.0

func save():
	var file = FileAccess.open_encrypted_with_pass(FILE_NAME, FileAccess.WRITE, ENCRYPTION_PASS)
	file.store_string(JSON.stringify(cache))
	file.close()

func load_data():
	if(FileAccess.file_exists(FILE_NAME)):
		var file = FileAccess.open_encrypted_with_pass(FILE_NAME, FileAccess.READ, ENCRYPTION_PASS)
		var data = JSON.parse_string(file.get_as_text())
		file.close()
		if typeof(data) == TYPE_DICTIONARY:
			cache = data
