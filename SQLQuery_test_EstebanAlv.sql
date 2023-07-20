--============ Crear DB ============

CREATE DATABASE serfinsaEmpleados;
USE serfinsaEmpleados;

--============ Crear tabla ============
CREATE TABLE Empleado (
	idEmpleado int primary key identity,
	nombreEmpleado varchar(50),
	apellidoEmpleado varchar(50),
	edadEmpleado int,
	direccionEmp varchar(100),
	telefonoEmp varchar (10),
	emailEmpleado varchar(30),
);

--============ Insertar Info de prueba ============

INSERT INTO Empleado(nombreEmpleado, apellidoEmpleado, edadEmpleado, direccionEmp, telefonoEmp, emailEmpleado) VALUES 
('Pedro', 'Gutierrez', 20, 'Chalatenango', '75760001', 'juan123@gmail.com'),
('Esteban', 'Alvarenga', 20, 'San Salvador', '75768136', 'estebanalvarenga2002@gmail.com');
Select * from Empleado;

--=========== Crear procedimientos almacenados ============

-- Listar Empleados
CREATE PROCEDURE sp_ListaEmpleados
as
begin
	select idEmpleado, nombreEmpleado, apellidoEmpleado, edadEmpleado, direccionEmp, telefonoEmp, emailEmpleado from Empleado
end

-- Guardar Empleados
CREATE PROCEDURE sp_GuardarEmpleado
(
	@nombreEmpleado varchar(50),
	@apellidoEmpleado varchar(50),
	@edadEmpleado int,
	@direccionEmp varchar(100),
	@telefonoEmp varchar (10),
	@emailEmpleado varchar(30)
)
as
begin 
	INSERT INTO Empleado(nombreEmpleado, apellidoEmpleado, edadEmpleado, direccionEmp, telefonoEmp, emailEmpleado) VALUES
	(@nombreEmpleado, @apellidoEmpleado, @edadEmpleado ,@direccionEmp, @telefonoEmp, @emailEmpleado)
end

-- Editar Empleados
CREATE PROCEDURE sp_EditarEmpleado (
    @idEmpleado int,
	@nombreEmpleado varchar(50),
	@apellidoEmpleado varchar(50),
	@edadEmpleado int,
	@direccionEmp varchar(100),
	@telefonoEmp varchar (10),
	@emailEmpleado varchar(30)
)
as
begin
	update Empleado set
	nombreEmpleado = @nombreEmpleado, 
	apellidoEmpleado = @apellidoEmpleado, 
	edadEmpleado = @edadEmpleado,
	direccionEmp = @direccionEmp, 
	telefonoEmp = @telefonoEmp, 
	emailEmpleado = @emailEmpleado
	WHERE idEmpleado = @idEmpleado
end

-- Eliminar Empleados
CREATE PROCEDURE sp_EliminarEmpleado
(
	@idEmpleado int
)
as
begin
	delete from Empleado where idEmpleado = @idEmpleado
end