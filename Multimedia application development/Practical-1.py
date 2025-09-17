from PIL import Image, ImageOps, ImageEnhance, ImageFilter


img = Image.open(r"B:\Games\OOP\Multimedia application development\Practic MAD.jpg")

w, h = img.size
new_w = 1920
new_h = int(h * (new_w / w))
img = img.resize((new_w, new_h))

# інверсія кольорів
img = ImageOps.invert(img.convert("RGB"))

# віддзеркалення
img = ImageOps.mirror(img)

# контраст ×1.5
enhancer = ImageEnhance.Contrast(img)
img = enhancer.enhance(1.5)

# виконати різкість
img = img.filter(ImageFilter.SHARPEN)

img.save(r"B:\Games\OOP\Multimedia application development\result8.png")

print("Файл збережено як result8.png")