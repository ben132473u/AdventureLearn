extends Node
var fb
var count = 0
var id
var email
var fb_name
signal info
func facebook_connect():
    if Engine.has_singleton("GodotFacebook"):
        fb = Engine.get_singleton("GodotFacebook")
        fb.init("1530305797147294")
        fb.setFacebookCallbackId(get_instance_id())
func getFbInfo():
    fb.getFbInfo()
func getName():
    return fb_name
    #return fb.getName()
func getId():
    return id
    #return fb.getId()
func getEmail():
    return email
    #return fb.getEmail()
func isLoggedIn():
    return fb.isLoggedIn()

func login():
    fb.login()

func logout():
    fb.logout()

func get_info():
    var test = fb.getArray()
    for item in test:
        print(item)
func get_dada(da:Array):
    id = da[0]
    email = da[1]
    fb_name = da[2]
    emit_signal("info")
    
