extends Node

var toast
var global = preload("res://Global/Global.cs");

func _ready():
	pass # Replace with function body.

func displayToast():
	if(Engine.has_singleton("GodotToast")):
		toast= Engine.get_singleton("GodotToast")
	var name = global.GetStudentName()
	var msg = "Welcome " + name
	toast.sendToast(msg)