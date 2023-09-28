window.onload = BuscarAsignatura();
function BuscarAsignatura(){
    $("#tbody-asignatura").empty();
    $.ajax({
        url: '../../Asignatura/BuscarAsignatura',
        type:'GET',
        dataType:"json",
        success: function(asignaturas){
            $("#tbody-asignatura").empty();
            $.each(asignaturas, function(index, asignatura){
                var BotonEliminar="";
                var botones = '<button type="button" onclick="EditarAsignatura(' + asignatura.asignaturaId+ ')" class="btn btn-primary"  title="Editar">Editar</button>'+
                '<button type="button" onclick="EliminarAsignatura(' + asignatura.asignaturaId  + ', 1)" class="btn btn-danger"  title="Eliminar">Eliminar</button>';
                $("#tbody-asignatura").append('<tr class"' + BotonEliminar+'">'
                + '<td class="text-center">'+ asignatura.nombreA + '</td>'
                + '<td class="text-center">'+ asignatura.carrera + '</td>'
                + '<td class="text-center">' + botones + '</td>' + '</tr>');
            });
        },
        error : function (xhr, status) {
            alert('Error al cargar las Asignaturas')
        },
    });
}
function VaciarFormulario(){
    $("#NombreA").val();
    $("#Carrera").val("");
    $("#AsignaturaId").val(0);
}
function EditarAsignatura(AsignaturaId){
    $.ajax({
        url: "../../Asignatura/BuscarAsignatura",
        data: {AsignaturaId: AsignaturaId},
        type: 'GET',
        dataType: 'json',
        success: function (asignaturas){
            if(asignaturas.length ==1){
                let asignatura = asignaturas [0];
                $("#NombreA").val(asignatura.nombreA);
                $("#Carrera").val(asignatura.carrera);
            }
            $("#modal").modal('show');
        },
        error: function(xhr, status){
            alert("Error al editar");
        }
    });
}
function GuardarAsignatura(){
    let AsignaturaId = $("#AsignaturaId").val();
    let NombreA= $("#NombreA").val();
    let Carrera=$("#Carrera").val();
    $.ajax({
        url: '../../Asignatura/GuardarAsignatura',
        data:{AsignaturaId: AsignaturaId, NombreA: NombreA, Carrera: Carrera},
        dataType:'POST',
        success: function(resultado){
            if(resultado){
                $("#ModalAsignatura").modal("hide");
                BuscarAsignatura();
            }
            else{
                alert('Ya existe esta asignatura')
            }
        },
        error:  function (xhr, status){alert ('No se pudo guardar')}
    })
}
function EliminarAsignatura(AsignaturaId, Eliminar)
{
    $.ajax({
        url:"../../Asignatura/EliminarAsignatura",
        data : {"AsignaturaId":AsignaturaId,"Eliminar" : Eliminar},
        type :"POST",
        dataType: "json",
        success:   function (resultado){
            if(resultado)
            {
                BuscarAsignatura();
                alert('Asignatura eliminada')
            }
        },
        error: function(xhr, status){ alert('No se pudo eliminar')}
    })
}