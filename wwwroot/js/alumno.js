window.onload = BuscarAlumno();
function BuscarAlumno(){
    $("#tbody-alumno").empty();
    $.ajax({
        url:'../../Alumno/BuscarAlumno',
        type: 'GET',
        dataType: 'json',
        success: function(alumnos){
            $("#tbody-alumno").empty();
            $.each(alumnos, function (Index, alumno){
                var BotonEliminar= '';
                var botones ='<button type="button" onclick="BuscarAlumnos(' + alumno.alumnoId + ')" class="btn" role="button" title="Editar"></button>'+
                    '<button type="button" onclick="EliminarClub(' + alumno.alumnoId  + ', 1)" class="btn btn-danger" role="button" title="Eliminar"></button>';
                
                $("#tbody-alumno").append('<tr class="' + BotonEliminar + '">' 
                + '<td class="text-center lt">' + alumno.nombre + '</td>' 
                + '<td class="text-center lt">' + alumno.localidad + '</td>' +
                '<td class="text-center lt">' + alumno.fechanacimiento + '</td>' + 
                '<td class="text-center lt">' + alumno.carrera + '</td>' +
                '<td class="text-center">' + botones + '</td>' + '</tr>');
            });
        },
        error : function (xhr, status){
            alert('Error al cargar el alumno')
        },
    });
}
function VaciarFormulario() {
    $("#Nombre").val('');
    $("#AlumnoId").val(0);
    $("#Localidad").val("");
    $("#FechaNacimiento").val("");
    $("#Carrera").val("");
}
//EDITAR
function BuscarAlumnos(AlumnoId){
    $.ajax({
        url:"../../Alumno/BuscarAlumno",
        data:{ AlumnoId: AlumnoId},
        type: 'GET',
        dataType: 'json',
        success: function(alumnos){
            if(alumnos.length ==1){
                let alumno = alumnos[0];
                $('#Nombre').val(alumno.nombre);
                $('#AlumnoId').val(alumno.alumnoId);
                $('#Localidad').val(alumno.localidad);
                $('#FechaNacimiento').val(alumno.fechanacimiento);
                $('#Carrera').val(alumno.carrera).change();
                }else{
                    console.log("No se encontro ningun registro");
                    }
                $("#ModalAlumno").modal("show");
            },
            error : function (xhr, status){
                alert('Error al editar el alumno');}
    });
}
//GUARDAR
function GuardarAlumno(){
    let AlumnoId = $("#AlumnoId").val();
    let Nombre = $("#Nombre").val();
    let Localidad = $("#Localidad").val();
    $.ajax({
        url: '../../Alumno/GuardarAlumno',
        data:{AlumnoId:AlumnoId, Nombre: Nombre, Localidad: Localidad},
        type: 'POST',
        dataType:'json',
        success: function(resultado){
            if(resultado){
                $("#ModalAlumno").modal("hide");
                BuscarAlumno();
            }
            else{
                alert("Ya existe este alumno")
            }
        },
        error : function (xhr, status){
            alert('No se pudo guardar el alumno')
        },
    })
}

function EliminarAlumno(AlumnoId, Eliminar){
    $.ajax({
        url:"../../Alumno/EliminarAlumno",
        data: {AlumnoId: AlumnoId, Eliminar: Eliminar},
        type: 'POST',
        dataType: "json" ,
        success: function(resultado){
            if(resultado){
                BuscarAlumno();
                console.log("alumno eliminado");
            }
        },
        error : function (xhr, status){
            alert('no se pudo eliminar el alumno')
        }
    });
}