window.onload = BuscarAlumno();
function BuscarAlumno() {
    $("#tbody-alumno").empty();
    $.ajax({
        url: '../../Alumno/BuscarAlumno',
        type: 'GET',
        dataType: 'json',
        success: function (alumnos) {
            $("#tbody-alumno").empty();
            $.each(alumnos, function (Index, alumno) {
                var BotonEliminar = '';
                var botones = '<button type="button" onclick="BuscarAlumnos(' + alumno.alumnoId + ')" class="button-81" role="button style="margin-right: 5%;" title="Editar">Editar</button>' +
                    '<button type="button" onclick="EliminarAlumno(' + alumno.alumnoId + ', 1)" class="button-82" role="button" style="margin-left: 5%;" title="Eliminar">Eliminar</button>';
                console.log(alumno)
                let fechas = new Date(alumno.fechaNacimiento);
                let fechaFormatted = fechas.toLocaleDateString('es-ES');
                $("#tbody-alumno").append('<tr class="' + BotonEliminar + '">'
                    + '<td class="text-center lt">' + alumno.nombre + '</td>'
                    + '<td class="text-center lt">' + fechaFormatted + '</td>'
                    + '<td class="text-center lt">' + alumno.dni + '</td>'
                    + '<td class="text-center lt">' + alumno.direccion + '</td>'
                    + '<td class="text-center lt">' + alumno.email + '</td>'
                    + '<td class="text-center lt">' + alumno.carreraNombre + '</td>'
                    + '<td class="text-center">' + botones + '</td>' + '</tr>');
            });
        },
        error: function (xhr, status) {
            alert('Error al cargar el alumno')
        },
    });
}
function VaciarFormulario() {
    $("#Nombre").val('');
    $("#AlumnoId").val(0);
    $("#Localidad").val("");
    $("#FechaNacimiento").val("");
    $("#DNI").val("");
    $("#Direccion").val("");
    $("#Email").val();
    $("#Carrera").val("");
}
//EDITAR
function BuscarAlumnos(AlumnoId) {
    $.ajax({
        url: "../../Alumno/BuscarAlumno",
        data: { AlumnoId: AlumnoId },
        type: 'GET',
        dataType: 'json',
        success: function (alumnos) {
            if (alumnos.length == 1) {
                let alumno = alumnos[0];
                $('#Nombre').val(alumno.nombre);
                $('#AlumnoId').val(alumno.alumnoId);
                $('#FechaNacimiento').val(alumno.fechanacimiento);
                $("#DNI").val(alumno.dni);
                $("#Direccion").val(alumno.direccion);
                $("#Email").val(alumno.email);
                $('#Carrera').val(alumno.Carrera);
            } else {
                console.log("No se encontro ningun registro");
            }
            $("#ModalAlumno").modal("show");
        },
        error: function (xhr, status) {
            alert('Error al editar el alumno');
        }
    });
}
//GUARDAR
function GuardarAlumno() {
    let AlumnoId = $("#AlumnoId").val();
    let Nombre = $("#Nombre").val();
    let FechaNacimiento = $("#FechaNacimiento").val();
    let DNI = $("#DNI").val();
    let Direccion = $("#Direccion").val();
    let Email= $("#Email").val();
    let CarreraId = $("#CarreraId").val();
    $.ajax({
        url: '../../Alumno/GuardarAlumno',
        data: { AlumnoId: AlumnoId, Nombre: Nombre, FechaNacimiento: FechaNacimiento, DNI:DNI, Direccion:Direccion, Email:Email, CarreraId: CarreraId },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado) {
                $("#ModalAlumno").modal("hide");
                BuscarAlumno();
            }
            else {
                alert("Ya existe este alumno")
            }
        },
        error: function (xhr, status) {
            alert('No se pudo guardar el alumno')
        },
    })
}

function EliminarAlumno(AlumnoId, Eliminar) {
    console.log("AlumnoId: ", AlumnoId, "Eliminar: ", Eliminar);

    $.ajax({
        url: "../../Alumno/EliminarAlumno",
        data: { AlumnoId: AlumnoId, Eliminar: Eliminar },
        type: 'POST',
        dataType: "json",
        success: function (resultado) {
            if (resultado) {
                BuscarAlumno();
                console.log("alumno eliminado");
                alert("Estudiante eliminado")
            }
        },
        error: function (xhr, status) {
            alert('no se pudo eliminar al estudiante')
        }
    })
}