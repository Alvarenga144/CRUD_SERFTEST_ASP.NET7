// Objeto de tipo empleado para facilitar la gestion
const _modeloEmpleado = {
    idEmpleado: 0,
    nombreEmpleado: "",
    apellidoEmpleado: "",
    edadEmpleado: 0,
    direccionEmp: "",
    telefonoEmp: "",
    emailEmpleado: "",
}


// Mostrar empleados dentro de la tabla
function MostrarEmpleados() {

    fetch("/Home/ListaEmpleados")
        .then(response => {
            console.log(response);
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaEmpleados tbody").html("");

                responseJson.forEach((empleado) => {
                    $("#tablaEmpleados tbody").append(
                        $("<tr>").append(
                            $("<td>").text(empleado.idEmpleado),
                            $("<td>").text(empleado.nombreEmpleado),
                            $("<td>").text(empleado.apellidoEmpleado),
                            $("<td>").text(empleado.edadEmpleado),
                            $("<td>").text(empleado.direccionEmp),
                            $("<td>").text(empleado.telefonoEmp),
                            $("<td>").text(empleado.emailEmpleado),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-empleado").text("Editar").data("dataEmpleado", empleado),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-empleado").text("Eliminar").data("dataEmpleado", empleado),
                            )
                        )
                    )
                })

            }

        })

}


// Manipulación del dom para mostrar la información del metodo MostrarEmpleados()
document.addEventListener("DOMContentLoaded", function () {

    MostrarEmpleados();

}, false)


// Mostrar Modal Estructura
function MostrarModal() {

    $("#txtidEmpleado").val(_modeloEmpleado.idEmpleado);
    $("#txtNombre").val(_modeloEmpleado.nombreEmpleado);
    $("#txtApellido").val(_modeloEmpleado.apellidoEmpleado);
    $("#txtedad").val(_modeloEmpleado.edadEmpleado);
    $("#txtDireccion").val(_modeloEmpleado.direccionEmp);
    $("#txtTelefono").val(_modeloEmpleado.telefonoEmp);
    $("#txtemail").val(_modeloEmpleado.emailEmpleado);

    $("#modalEmpleado").modal("show");
}

// Abrir modal mediante el botón
$(document).on("click", ".boton-nuevo-empleado", function () {

    _modeloEmpleado.idEmpleado = 0,
    _modeloEmpleado.nombreEmpleado=  "",
    _modeloEmpleado.apellidoEmpleado=  "",
    _modeloEmpleado.edadEmpleado = 0,
    _modeloEmpleado.direccionEmp =  "",
    _modeloEmpleado.telefonoEmp = "",
    _modeloEmpleado.emailEmpleado= "",

    MostrarModal();
});


// Llenar inputs del modal al hacer click en el boton de editar

$(document).on("click", ".boton-editar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");

    _modeloEmpleado.idEmpleado = _empleado.idEmpleado,
    _modeloEmpleado.nombreEmpleado = _empleado.nombreEmpleado,
    _modeloEmpleado.apellidoEmpleado = _empleado.apellidoEmpleado,
    _modeloEmpleado.edadEmpleado = _empleado.edadEmpleado,
    _modeloEmpleado.direccionEmp = _empleado.direccionEmp,
    _modeloEmpleado.telefonoEmp = _empleado.telefonoEmp,
    _modeloEmpleado.emailEmpleado = _empleado.emailEmpleado,

    MostrarModal();
})


// Guardar datos del modal y Editar, se usa el mismo botón para ambos
$(document).on("click", ".boton-guardar-cambios-empleado", function () {

    const modelo = {

        idEmpleado: _modeloEmpleado.idEmpleado,
        nombreEmpleado: $("#txtNombre").val(),
        apellidoEmpleado: $("#txtApellido").val(),
        edadEmpleado: $("#txtedad").val(),
        direccionEmp: $("#txtDireccion").val(),
        telefonoEmp: $("#txtTelefono").val(),
        emailEmpleado: $("#txtemail").val(),
    }

    // Condicional para verificar si el id del empleado es igual a cero, es que se agregará uno nuevo
    // de lo contrario, si el id es mayor, es que debe editarse
    if (_modeloEmpleado.idEmpleado == 0) {

        fetch("/Home/GuardarEmpleados", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue creado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Home/EditarEmpleados", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue actualizado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })
    }
})


// Eliminar data del Empleado
$(document).on("click", ".boton-eliminar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminarás a: ${_empleado.nombreEmpleado} ${_empleado.apellidoEmpleado}`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/EliminarEmpleados?idEmpleado=${_empleado.idEmpleado}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Empleado fue elminado", "success");
                        MostrarEmpleados();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })
        }
    })
})