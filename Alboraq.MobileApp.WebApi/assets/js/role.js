(function ($, widgetObject) {
    var controls = {
        appModal: $('#appModal'),
        appModalContent: $('#appModal .modal-content'),
        btnAddRoleDialog: $('#btnAddRoleDialog'),
        btnEditRoleDialog: $('.btnEditRoleDialog'),
        btnAddUserToRoleDialog: $('.btnAddUserToRoleDialog')
    };

    widgetObject.init = function () {
        bindRoleUIActions();
    };

    var createModalDialog = function (args) {
        args.modaltitle = args.modalTitle || undefined;
        args.modalSize = args.modalSize || undefined;

        if (args.modaltitle !== undefined) {
            controls.appModal.find('.modal-title').text(args.modalTitle);
        }

        if (args.modalSize !== undefined) {
            controls.appModal.find('.modal-dialog').addClass(args.modalSize);
        }

        controls.appModal.find('.modal-footer')
            .append('<button id="btnClose" type="button" class="btn btn-default">Close</button>');
    };

    var toggleEnableButton = function (args) {
        args.button = args.button || undefined;
        args.innerHTML = args.innerHTML || undefined;
        args.disabled = args.disabled || false;

        var button = controls.appModal.find(args.button);

        if (args.innerHTML !== undefined && button.length > 0) {
            button.html(args.innerHTML);
        }

        if (args.disabled && button.length > 0) {
            button.addClass('disabled');
            controls.appModal.find('#btnClose').addClass('disabled');
            controls.appModal.find('.close').removeClass('hidden');
        } else {
            button.removeClass('disabled');
            controls.appModal.find('#btnClose').removeClass('disabled');
            controls.appModal.find('.close').removeClass('hidden');
        }
    };

    var createAlertDialog = function () {
        var dialog;

        dialog = controls.appModal.find('#alertMessage');

        if (dialog.length > 0)
            dialog.removeClass('alert-success alert-danger').empty();
        else
            dialog = $('<div id="alertMessage" class="alert"></div>');

        return dialog;
    };

    var addRoleAsync = function (e) {

        e.preventDefault();

        var form = $(e.currentTarget),
            alertDialog;

        toggleEnableButton({
            button: '#btnAddRole',
            innerHTML: 'Adding role..&nbsp; <i class="fa fa-spinner fa-pulse"></i>',
            disabled: true
        });

        alertDialog = createAlertDialog();

        $.ajax({
            method: 'post',
            url: '/admin/addrole',
            data: form.serialize(),
            success: function (response) {
                if (form.valid()) {
                    if (response.isSuccess) {
                        alertDialog.addClass('alert-success');
                        alertDialog.append('<p class="text-center">' + response.message + '</p>');
                    } else {
                        alertDialog.addClass('alert-danger');
                        $.each(response.errors, function (key, value) {
                            alertDialog.append('<p class="text-center">' + value + '</p>');
                        });
                    }
                    controls.appModal.find('.modal-body').prepend(alertDialog);
                    controls.appModal.find('input').val('').focus();
                }
                toggleEnableButton({
                    button: '#btnAddRole',
                    innerHTML: 'Submit',
                    disabled: false
                });
            },
            error: function (jqXHR) {
                alertDialog.addClass('alert-danger');
                alertDialog.append('<p class="text-center">' + jqXHR.responseText + '</p>');
            }
        });
    };

    var updateRoleAsync = function (e) {
        e.preventDefault();

        var form = $(e.currentTarget),
            alertDialog;

        toggleEnableButton({
            button: '#btnUpdateRole',
            innerHTML: 'Updating..&nbsp;<i class="fa fa-spinner fa-pulse"></i>',
            disabled: true
        });

        alertDialog = createAlertDialog();

        $.ajax({
            method: 'post',
            url: '/admin/updaterole',
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    alertDialog.addClass('alert-success')
                        .append('<p class="text-center">' + response.message + '</p>');
                } else {
                    $.each(response.errors, function (key, value) {
                        alertDialog.addClass('alert-danger')
                            .append('<p class="text-center">' + value + '</p>');
                    });
                }
                controls.appModal.find('.modal-body').prepend(alertDialog);

                toggleEnableButton({
                    button: '#btnUpdateRole',
                    innerHTML: 'Update',
                    disabled: false
                });
            },
            error: function (jqXHR) {
                alertDialog.addClass('alert-danger')
                    .append('<p class="text-center">' + jqXHR.responseText + '</p>');
            }
        })
    }

    var bindRoleUIActions = function () {
        controls.appModal.modal({
            backdrop: 'static',
            keyboard: false,
            show: false
        });

        controls.appModal.on('shown.bs.modal', function () {
            if ($('#RoleName')) {
                $('#RoleName').focus();
            }
        });

        controls.btnAddRoleDialog.on('click', function () {

            createModalDialog({
                modalTitle: 'Add role',
                modalSize: 'modal-sm'
            });

            var modalBody = controls.appModal.find('.modal-body');

            modalBody.append('<p class="text-center"><i class="fa fa-spinner fa-pulse fa-2x"></i></p>');

            modalBody.load('/admin/NewRolePartialView', function () {
                controls.frmAddRole = controls.appModal.find('#frmAddRole');

                controls.frmAddRole.validate({
                    rules: {
                        RoleName: 'required'
                    },
                    messages: {
                        RoleName: 'Please enter a role name'
                    },
                    submitHandler: function (form) {
                        var formAddRole = $(form);

                        if (!formAddRole.valid()) return;

                        formAddRole.submit(addRoleAsync(event));
                    }
                });
            });
            controls.appModal.modal('show');
        });

        controls.appModalContent.on('click', '#btnClose', function () {
            controls.appModal.modal('hide');
            window.location.reload();
        });

        controls.btnEditRoleDialog.on('click', function (e) {
            var btn = $(e.currentTarget),
                roleID = btn.data('roleid');

            createModalDialog({
                modaltitle: 'Edit role',
                modalSize: 'modal-sm'
            });

            var modalBody = controls.appModal.find('.modal-body');
            modalBody.append('<p class="text-center"><i class="fa fa-spinner fa-pulse fa-2x"></i></p>');

            modalBody.load('/admin/EditRolePartialView?roleID=' + roleID, function () {
                controls.frmUpdateRole = controls.appModal.find('#frmUpdateRole');

                controls.frmUpdateRole.validate({
                    rules: {
                        RoleName: 'required'
                    },
                    messages: {
                        RoleName: 'Please enter a role name'
                    },
                    submitHandler: function (form) {
                        var formUpdateRole = $(form);


                        if (!formUpdateRole.valid()) return;

                        formUpdateRole.submit(updateRoleAsync(event));
                    }
                });
            });
            controls.appModal.modal('show');
        });
    }
})(jQuery, window.RoleWidget = window.RoleWidget || {});

RoleWidget.init();