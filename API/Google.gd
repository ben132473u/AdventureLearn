extends Node
var google_play
var fb

func google_connect():
    if Engine.has_singleton("PlayGames"):
        google_play = Engine.get_singleton("PlayGames")
        google_play.google_init(get_instance_id())
        google_play.connect()

func google_disconnect():
    google_play.disconnect()
func get_email():
    return google_play.getEmail()
func get_id():
    return google_play.getId()
func get_name():
    return google_play.getName()
func check_status():
    return google_play.checkStatus()
func achievement_unlock(achivementId):
    google_play.achievementUnlock(achivementId)
func achievement_show():
    google_play.achievementShowList()
