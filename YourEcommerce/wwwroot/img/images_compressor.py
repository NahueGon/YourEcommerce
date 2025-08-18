import os
from PIL import Image

# Rutas de tus carpetas
carpeta_origen = "women"
carpeta_destino = "women_compressed"

# Crear carpeta de destino si no existe
os.makedirs(carpeta_destino, exist_ok=True)

# Nivel de compresión (0-100), donde 100 = máxima calidad
calidad = 70

# Extensiones soportadas
extensiones = ('.jpg', '.jpeg', '.png')

# Proceso de conversión
for archivo in os.listdir(carpeta_origen):
    if archivo.lower().endswith(extensiones):
        ruta_origen = os.path.join(carpeta_origen, archivo)

        # Eliminar extensión y agregar .webp
        nombre_sin_extension = os.path.splitext(archivo)[0]
        nombre_webp = nombre_sin_extension + ".webp"
        ruta_destino = os.path.join(carpeta_destino, nombre_webp)

        with Image.open(ruta_origen) as img:
            img.convert("RGB").save(ruta_destino, format="WEBP", quality=calidad, optimize=True)

print("✅ Conversión completa a WebP con compresión.")