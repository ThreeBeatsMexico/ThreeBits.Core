$(document).ready(function () {
    var validAjax;
    var formAjax = $('.formAjaxApps');
    $(".btnAjaxApps").on('click', function (e) {
        e.preventDefault();
        // alert("ENTRA EN EL CLICK");



        validAjax = formAjax.validate({
            //== Validate only visible fields
            ignore: ":hidden",
            //== Display error  
            invalidHandler: function (event, validAjax) {
                PNotify.error({
                    title: 'Oh No!',
                    text: 'Los datos capturados no son correctos, por favor valida'
                });
            },
            //== Submit valid form
            submitHandler: function (form) {
            }
        });
        if (validAjax.form() == false) {
            $("#userSubs-error").hide();
            return;
        }
        $(".loadingAjax").show();
        formAjax.ajaxSubmit({
            success: function (response) {
                if (response.IsSuccess == true) {
                    if (response.Id == -1) {
                        window.location.href = response.Message;
                    } else {
                        PNotify.success({
                            title: 'Operacion completada!',
                            text: response.Message,
                            icon: 'fas fa-info-circle',
                            hide: false,
                            modules: {
                                Confirm: {
                                    confirm: true,
                                    buttons: [{
                                        text: 'Ok',
                                        primary: true,
                                        click: function (notice) {
                                            notice.close();
                                            if (response.Result != null) {
                                                window.location = urlGeneral + "Aplicaciones";
                                            }
                                        }
                                    }]
                                },
                                Buttons: {
                                    closer: false,
                                    sticker: false
                                },
                                History: {
                                    history: false
                                }
                            }
                        });
                    }
                }
                else {
                    PNotify.error({
                        title: 'Error',
                        text: response.Message,
                        icon: 'fas fa-info-circle',
                        hide: false,
                        modules: {
                            Confirm: {
                                confirm: true,
                                buttons: [{
                                    text: 'Ok',
                                    primary: true,
                                    click: function (notice) {
                                        notice.close();
                                    }
                                }]
                            },
                            Buttons: {
                                closer: false,
                                sticker: false
                            },
                            History: {
                                history: false
                            }
                        }
                    });
                }
                $(".loadingAjax").hide();
            },
            error: function (request, status, error) {
                swal({
                    "title": "",
                    "text": "No se puede conectar al servidor, intentelo más tarde!" + request.responseText,
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                }).then(function () {
                    //window.location = urlGeneral + "tramites";
                });
                $(".loadingAjax").hide();
            }
        });
    });
});








$('#e-commerce-products-table').DataTable({
    responsive: true,
    dom: 'rt<"dataTables_footer"ip>',
    search: true

});

//$('#mdlAplicacion').on('shown.bs.modal', function () {
//    $('#recipient-name').trigger('focus');
//    $("#mdlAplicacion").css("z-index", "1500");
//    if ($('.modal-backdrop').is(':visible')) {
//        // $('body').removeClass('modal-open');
//        $('.modal-backdrop').css("z-index", "1000");
//    };
//});

$("#btnNewApp").click(function (eve) {
    $('#modal-content').load("Aplicaciones/Create");
    $('#mdlTitle').text("Nueva Aplicacion");
});

$(".btnEditApp").click(function (eve) {
   // alert("etnra, parametro: " + eve);
    $('#modal-content').load("Aplicaciones/Edit/" + $(this).data("id"));
    $('#mdlTitle').text("Editar Aplicacion");
});

function myFunction() {
    var x = document.getElementById("sPasswordApp");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

//$(document).ready(function () {

//    $('#mdlAplicacion').on('hidden.bs.modal', function (e) {

//        alert("entra");
//        $('#modal-content').removeData('bs.modal');
//        $('#modal-content').empty();
//    })

//})

