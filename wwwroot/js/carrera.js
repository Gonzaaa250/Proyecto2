window.onload = BuscarCarrera();
function BuscarCarrera(){
    $("#tbody-carrera").empty();
    $.ajax({
        url: '../../Carrera/BuscarCarrera',
        type: 'GET',
        dataType:'json',
        success : function(carreras){
            $("#tbody-carrera").empty();
            $.each(carreras, function(Index, carrera){
                var BotonEliminar="";
                var botones= '<button type="button" onclick="BuscarCarreras(' + carrera.carreraId + ')" class="button-81" role="button style="margin-right: 5%;" title="Editar">Editar</button>'+
                '<button type="button" onclick="GuardarCarrera(' + carrera.carreraId  + ', 1)" class="button-82" role="button" style="margin-left: 5%;" title="Eliminar">Eliminar</button>';
                $("#tbody-carrera").append('<tr class="' + BotonEliminar + '">'
                + '<td class="text-center lt">' + carrera.nombre + '</td>' 
                + '<td class="text-center lt">' + carrera.duracion + '</td>' + 
                '<td class="text-center">' + botones + '</td>'+'</tr>');
            });
        },
        error: function(xhr, status){
            alert('Error al cargar la carrera')
        },
    });
}
function VaciarFormulario() {
    $("#Nombre").val('');
    $("#CarreraId").val(0);
    $("#Duracion").val("");
}
//EDITAR
function BuscarCarreras(CarreraId){
    $.ajax({
        url:'../../Carrera/BuscarCarrera',
        data:{CarreraId: CarreraId},
        type: 'GET',
        dataType: 'json',
        success: function (carreras) {
            if(carreras.length ==1){
                let carrera = carreras[0];
                $('#Nombre').val(carrera.nombre);
                $('#Duracion').val(carrera.duracion);
                $('#CarreraId').val(carrera.carreraId);

                $("#ModalCarrera").modal("show");
            }
        },
        error: function(xhr, status){
            alert ("Error al editar la carrera")
        }
    });
}
//GUARDAR
function GuardarCarrera(){
    let CarreraId = $("#CarreraId").val();
    let Nombre = $("#Nombre").val();
    let Duracion = $("#Duracion").val();
    $.ajax({
        url: '../../Carrera/GuardarCarrera',
        data:{'CarreraId':CarreraId,'Nombre':Nombre,"Duracion":Duracion},
        type: 'POST',
        dataType: 'json',
        success: function(resultado){
            if (resultado){
                $("#ModalCarrera").modal("hide");
                BuscarCarrera();
            }
            else{
                alert("Ya existe la carrera")
            }
        },
        error: function(xhr, status){
            alert('No se pudo guardar la carrera')
        },
    })
}
//ELIMINAR
function EliminarCarrera(CarreraId, Eliminar){
    $.ajax({
        url:"../../Carrera/EliminarCarrera",
        data: {CarreraId: CarreraId, Eliminar: Eliminar},
        type:'POST',
        dataType : "json",
        success: function (resultado) {
            if(resultado){
                BuscarCarrera();
                console.log("carrera eliminada");
            }
        },
        error: function (xhr, status){
            alert('no se pudo eliminar la carrera')
        }
    })
}