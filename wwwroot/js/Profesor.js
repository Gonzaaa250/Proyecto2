window.onload = BuscarProfesor();
function BuscarProfesor(){
    $("#tbody-profesor").empty();
    $.ajax({
        url: '../../Profesor/BuscarProfesor',
        type:'GET',
        datatype: 'json',
        success : function(profesores){
            $("#tbody-profesor").empty();
            $.each(profesores, function(Index, profesores){
                var BotonEliminar ="";
                var botones = '<button type="button" onclick="BuscarProfesores(' + profesor.profesorId + ')" class="button-81" role="button style="margin-right: 5%;" title="Editar">Editar</button>'+
                '<button type="button" onclick="GuardarProfesor(' + profesor.profesorId + ', 1)" class="button-82" role="button" style="margin-left: 5%;" title="Eliminar">Eliminar</button>';
                $("#tbody-profesor").append('<tr class="' + BotonEliminar + '</td>'
                + '<td class="text-center lt"' + profesor.nombreP + '</td>'
                + '<td class="text-center lt"' + profesor.dni + '</td>'
                + '<td class="text-center lt"' + profesor.fechanacimiento + '</td>'
                + '<td class="text-center lt"' + profesor.direccion + '</td>'
                + '<td class="text-center lt"' + profesor.email + '</td>'
                +'<td class="text-center">' + botones + '</td>'+'</tr>')
            });
        },
        error : function (xhr, status){or
            alert('Error al cargar el Profesor')
        },
    });
}
function VaciarFormulario() {
    $("#Nombre").val('');
    $("#ProfesorId").val(0);
    $("#DNI").val('');
    $("#FechaNacimiento").val('');
    $("#Direccion").val('');
    $("#Email").val('');
}
//EDITAR
function BuscarProfesores(){
    $.ajax({
        url: '../../Profesor/BuscarProfesor',
        data: {profesorId : ProfesorId},
        type: 'GET',
        datatype: 'json',
        success : function(profesores){
            if(profesores.length ==1){
                let profesor = profesores[0];
                $("#NombreP").val(profesor.nombreP);
                $("#ProfesorId").val(profesor.profesorId);
                $("#DNI").val(profesor.dni);
                $("#Direccion").val(profesor.direccion);
                $("#Email").val(profesor.email);

                $("#ModalProfesor").modal("show");
            }
        },
        error: function(xhr, status){
            alert('Error al editar')
        }
    });
}
//Guardar
function GuardarProfesor(){
    let ProfesorId = $("#ProfesorID").val();
    let NombreP = $("#NombreP").val();
    let DNI = $("#DNI").val();
    let FechaNacimiento= $("#FechaNacimiento").val();
    let Email=$("#Email").val();
    let Direccion=$("#Direccion").val();
    $.ajax({
        url:'../../Profesor/GuardarProfesor',
        data:{ ProfesorId: ProfesorId, NombreP: NombreP, DNI: DNI, FechaNacimiento: FechaNacimiento, Email: Email, Direccion: Direccion},
        type: 'POST',
        datatype: 'json',
        success: function(resultado){
            if(resultado){
                $("#ModalProfesor").modal("hide");
            }
            else{
                alert('No se guardo correctamente');
            }
        },
        error: function (xhr, status){
            alert('error ')
        },
    })
}
//ELIMINAR
function EliinarProfesor(ProfesorId, Eliminar){
    $.ajax({
        url:'../../Profesor/EliminarProfesor',
        data:{ProfesorId: ProfesorId, Eliminar: Eliminar},
        type: 'POST',
        datatype:'json',
        success: function(resultado) {
            if(resultado){
                BuscarProfesor();
            }
        },
        error: function(xhr, status){
            alert('no se pudo eliminar el profesor')
        }
    })
    

}