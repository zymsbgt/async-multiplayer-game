extends TextureProgress

func _process(_delta):
	if visible:
		if value >= max_value:
			value = 0

		value += 75 * _delta
