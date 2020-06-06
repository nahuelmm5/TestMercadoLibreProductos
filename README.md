# TestMercadoLibreProductos

La aplicación recibe como parámetro, por cada user, los respectivos seller_id y site_id separados por una virgulilla (~).

Ejemplo de formato de llamada a la aplicación con un sólo usuario como parámetro:
C:\>ProductosMeli.exe 179571326~MLA

Ejemplo de formato de llamada a la aplicación con más de un usuario como parámetro:
C:\>ProductosMeli.exe 179571326~MLA 179571325~MLA

Crea un archivo log por cada usuario-site en el directorio donde se encuentra el ejecutable, con el nombre "log_{seller_id}.log".
