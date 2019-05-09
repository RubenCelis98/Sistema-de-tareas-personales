create database SistemaWebLogin
use SistemaWebLogin

create table RegCue(
IdUsu int primary key identity,
Nombre nvarchar(50),
Email nvarchar(50),
Telefono nvarchar(20),
Direccion nvarchar(200),
Contrasena nvarchar(50),
)

select *
from RegCue