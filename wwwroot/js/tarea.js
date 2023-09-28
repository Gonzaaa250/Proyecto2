window.onload = BuscarTarea();
function BuscarTarea(){
    $("#tbody-tarea").empty();
    $.ajax({
        url: '../../Tarea/BuscarTarea',
        type:'GET',
        dataType: 'json',
        success: function(tareas){
            $("#tbody-tarea").empty();
            $.each(tareas, function(index, tarea){
                var BotonEliminar= "";
                var botones = '<button type="button" onclick="EditarTareas(' + tarea.tareaId + ')" class="btn btn-primary"  title="Editar">Editar</button>'+
                '<button type="button" onclick="EliminarTarea(' + tarea.tareaId  + ', 1)" class="btn btn-danger"  title="Eliminar">Eliminar</button>';
                $("#tbody-tarea").append('<tr class"' + BotonEliminar+'">'
                + '<td class="text-center">'+ tarea.titulo + '</td>'
                + '<td class="text-center">'+ tarea.descripcion + '</td>'
                + '<td class="text-center">'+ tarea.fechacreacion+'</td>'
                + '<td class="text-center">'+tarea.fechavencimiento+'</td>'
                +'<td class="text-center">'+tarea.asignatura+'</td>'
                + '<td class="text-center">' + botones + '</td>' + '</tr>');
            });
        },
        error : function(xhr, status){
            alert ('Error al cargar la Tarea')
        },
    });
}
function VaciarFormulario(){
    $("#Titulo").val();
    $("#Descripcion").val("");
    $("#TareaId").val(0);
    $("#FechaCreacion").val("");
    $("#FechaVencimiento").val("");
}
//Editar
function EditarTareas(TareaId){
    $.ajax({
        url: '../../Tarea/BuscarTarea',
        data: {TareaId : TareaId},
        type: 'GET',
        dataType: 'json',
        success: function (tareas){
            if(tareas.length ==1){
                let tarea = tareas[0];
                $("#Titulo").val(tarea.titulo) ;  
                $("#Descripcion").val(tarea.descripcion);
                $("#FechaCreacion").val(tarea.fechacreacion);
                $("#FechaVencimiento").val(tarea.fechavencimiento);
                $("#Asignatura").val(tarea.asignatura);
            }
            $('#modal').modal('show');
        },
        error : function (xhr, status){
            alert("Error al editar la Tarea");
        }
    });
}
function GuardarTarea(){
    let TareaId = $("#TareaId").val();
    let Titulo = $("#Titulo").val() ;
    let Descripcion=$("#Descripcion").val();
    let FechaCreacion=$("#FechaCreacion").val();
    let FechaVencimiento= $("#FechaVencimiento").val();
    let Asignaturas = $("#Asignaturas").val();
    $.ajax({
        url:'../../Tarea/GuardarTarea',
        data:{ TareaId: TareaId, Titulo: Titulo, Descripcion: Descripcion, FechaCreacion: FechaCreacion, FechaVencimiento: FechaVencimiento, Asignaturas: Asignaturas},
        dataType: 'POST',
        success: function(resultado){
            if(resultado){
                $("#ModalTarea").modal("hide");
                BuscarTarea();
            }
            else{
                alert("Ya existe esta Tarea")
            }
        },
        error : function(xhr,status){
            alert("No se pudo guardar la tarea")
        },
    })
}
function EliminarTarea(TareaId, Eliminar)
{
    $,ajax({
        url: "../../Tarea/EliminarTarea",
        data:{TareaId: TareaId, Eliminar: Eliminar},
        type: 'POST',
        dataType:"json",
        success: function(resultado){
            if(resultado){
                BuscarTarea();
                alert("Tarea Eliminado")
            }
        },
        error: function(xhr, status){
            alert("No se pudo eliminar")
        }
    })
}