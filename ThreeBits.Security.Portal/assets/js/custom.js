$(document).ready(function () {
    var validAjax;
    var formAjax = $('.formAjax');
    $(".btnAjax").on('click', function (e) {
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
                                            window.location = urlGeneral + "Home";
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



    //forms add
    var validAjaxAdd;
    var formAjaxAdd = $('.formAjaxAdd');
    $("button.btnAjaxAdd").on('click', function (e) {
        e.preventDefault();
        validAjaxAdd = formAjaxAdd.validate({
            //== Validate only visible fields
            ignore: ":hidden",
            //== Display error  
            invalidHandler: function (event, validAjax) {
                swal({
                    "title": "",
                    "text": "Los datos capturados no son correctos.",
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                });
            },
            //== Submit valid form
            submitHandler: function (form) {
            }
        });
        if (validAjaxAdd.form() == false) {
            $("#userSubs-error").hide();
            return;
        }
        $(".loadingAjaxAdd").show();
        formAjaxAdd.ajaxSubmit({
            success: function (response) {
                if (response.IsSuccess == true) {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "success",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        $(".ocultarDespues").modal('hide');
                        cargaTabla(strFiltro, strValOrder);
                        removeInfoForm(formAjaxAdd);

                        //$("#IdMod").val(response.Id);
                        //window.location = urlGeneral + "tramites";
                    });
                }
                $(".loadingAjaxAdd").hide();
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
                $(".loadingAjaxAdd").hide();
            }
        });
    });

    function removeInfoForm(formAjaxAdd) {
        $(formAjaxAdd).find("input[type=text]").val("");
        $(formAjaxAdd).find("input[type=number]").val("");
        $(formAjaxAdd).find("input[type=number]")
        $("#modal-dropzone").find("input[type=file]").empty();
        $("#modal-dropzone").removeClass("dz-started");
        $(".dz-preview").remove();
        $(formAjaxAdd).find("select").val("");
    }

    function funProd(id, tipo, evt) {
        evt.preventDefault();
        if (tipo == 'E') {
            $.ajax({
                type: "POST",
                url: urlGeneral + "Productos/Eliminar",
                data: "{Id: " + id + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    swal({
                        "title": "",
                        "text": response.Message,
                        "type": "success",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    }).then(function () {
                        $(".ocultarDespues").modal('hide');
                        cargaTabla(strFiltro, strValOrder);
                        removeInfoForm(formAjaxAdd);
                    });
                    $(".loadingAjaxAdd").hide();
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
                    $(".loadingAjaxAdd").hide();
                }
            });
        } else {
            $("#modal8").modal("show");
            $.ajax({
                type: "POST",
                url: urlGeneral + "Productos/Get",
                data: "{Id: " + id + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.IsSuccess == true) {
                        $("#Id").val(response.Result.Id);
                        $("#archivos").val(response.Result.archivos);
                        $("#txtCodigo").val(response.Result.Codigo);
                        $("#txtNombre").val(response.Result.Nombre);
                        $("#txtDescripcion").val(response.Result.Descripcion);
                        $("#txtMarca").val(response.Result.Marca);
                        $("#txtModelo").val(response.Result.Modelo);
                        $("#txtStock").val(response.Result.Stock);
                        $("#txtUnidadMedida").val(response.Result.UnidadMedida);
                        $("#txtCosto").val(response.Result.Costo);
                        $("#txtPrecioVenta").val(response.Result.PrecioVenta);
                        var arrayDeCadenas = response.Result.archivos.split("|");
                        var strImgs = "";
                        for (var i = 0; i < arrayDeCadenas.length; i++) {
                            if (arrayDeCadenas[i]) {
                                var funEli = 'eliminarImagen("' + arrayDeCadenas[i] + '", "divImagen' + i + '")';
                                strImgs += "<div id='divImagen" + i + "'><img src='/Uploads/" + arrayDeCadenas[i] + "' /><br/><a class='c-btn c-btn--danger' href='#!' onclick='" + funEli + "'><i class='fa fa-trash-o u-mr-xsmall'></i></a></div>";
                            }
                        }
                        if (strImgs != "") {
                            $("#anexosUpload").append(strImgs);
                            $("#anexosUpload").show();
                        }

                    } else {
                        swal({
                            "title": "",
                            "text": response.Message,
                            "type": "error",
                            "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                        }).then(function () {
                            //window.location = urlGeneral + "tramites";
                        });
                    }
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
                    $(".loadingAjaxAdd").hide();
                }
            });
        }
    }

    function eliminarImagen(val, divEliminar) {
        $("#archivos").val($("#archivos").val().replace(val + "|", ""));
        $("#" + divEliminar).remove();
    }

    $('#modal8').on('hidden.bs.modal', function () {
        $("input").val("");
        $("#anexosUpload").empty();
    });



   



});