(function ($, widgetObject) {
    var controls = {
        appModal: $('#appModal'),
        appModalContent: $('#appModal .modal-content'),
        btnAddUserDialog: $('#btnAddUserDialog'),
        btnEditUserDialog: $('.btnEditUserDialog'),
        btnResetPasswordDialog: $('.btnResetPasswordDialog'),
        btnDeleteUserDialog: $('.btnDeleteUserDialog')
    };

    widgetObject.init = function () {
        bindUiActions();
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
            .append('<button id="btnCloseDialog" type="button" class="btn btn-default">Close</button>');
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
            controls.appModal.find('#btnCloseDialog').addClass('hidden');
            controls.appModal.find('.close').addClass('hidden');
        } else {
            button.removeClass('disabled');
            controls.appModal.find('#btnCloseDialog').removeClass('hidden');
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

    var addUserAsync = function (e) {
        e.preventDefault();
        var form = $(e.currentTarget),
            alertDialog;

        toggleEnableButton({
            button: '#btnAddSubmit',
            innerHTML: 'Adding new user.. &nbsp; <i class="fa fa-spinner fa-pulse"></i>',
            disabled: true
        });

        alertDialog = createAlertDialog();

        $.ajax({
            method: 'post',
            url: '/api/account/register',
            data: form.serialize(),
            success: function (response) {
                //append response content
                alertDialog.addClass('alert-success')
                    .append('<p class="text-center">User ' + response + ' has been created!</p>');

                //append the dialog on the top of the form
                controls.appModal.find('.modal-body').prepend(alertDialog);

                //clear the input
                controls.appModalContent.find('#frmNewUser').find('input').val('');

                toggleEnableButton({
                    button: '#btnAddSubmit',
                    innerHTML: 'Submit',
                    disabled: false
                });

            },
            error: function (jqXHR) {

                //get the empty string property of the ModelState
                var modelStateErrors = jqXHR.responseJSON.ModelState[""];

                //loop through
                $.each(modelStateErrors, function (key, value) {
                    alertDialog.addClass('alert-danger')
                        .append('<p class="text-center">' + value + '</p>');
                });

                // append on the top
                controls.appModal.find('.modal-body').prepend(alertDialog);

                toggleEnableButton({
                    button: '#btnAddSubmit',
                    innerHTML: 'Submit',
                    disabled: false
                });
            }
        });
    }

    var updateUserAsync = function (e) {
        e.preventDefault();

        var form = $(e.currentTarget),
            alertDialog;

        toggleEnableButton({
            button: '#btnUpdateUser',
            innerHTML: 'Updating... &nbsp; <i class="fa fa-spinner fa-pulse"></i>',
            disabled: true
        });

        alertDialog = createAlertDialog();

        $.ajax({
            method: 'post',
            url: '/admin/updateuser',
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    alertDialog.addClass('alert-success')
                        .append('<p class="text-center">' + response.message + '</p>');
                    controls.appModal.find('.modal-body').prepend(alertDialog);

                    toggleEnableButton({
                        button: '#btnUpdateUser',
                        innerHTML: 'Update',
                        disabled: false
                    });
                } else {
                    $.each(response.errors, function (key, value) {
                        alertDialog.addClass('alert-danger')
                            .append('<p class="text-center">' + value + '</p>');

                        toggleEnableButton({
                            button: '#btnUpdateUser',
                            innerHTML: 'Update',
                            disabled: false
                        });
                    });
                    controls.appModal.find('.moda-body').prepend(alertDialog);
                }
            },
            error: function (jqXHR) {
                alertDialog.add('alert-danger')
                    .append('<p class="text-center">' + jqXHR.responseText + '</p>');
            }
        });
    }

    var bindUiActions = function () {

        controls.appModal.modal({
            backdrop: 'static',
            keyboard: false,
            show: false
        });

        controls.btnAddUserDialog.on('click', function () {

            createModalDialog({
                modalTitle: 'Add new user',
                modalSize: 'modal-sm'
            });

            var appModalBody = controls.appModal.find('.modal-body');

            appModalBody.append('<p class="text-center"><i class="fa fa-spinner fa-pulse fa-2x"></i></p>');

            appModalBody.load('/admin/NewUserPartialView', function () {
                //add the form element in the controls object
                controls.frmNewUser = controls.appModalContent.find('#frmNewUser');

                //bind the validation when the form is loaded.
                controls.frmNewUser.validate({
                    rules: {
                        Name: 'required',
                        Email: {
                            required: true,
                            //validated by the built-in email rule
                            email: true
                        },
                        Password: {
                            required: true,
                            minlength: 6
                        },
                        ConfirmPassword: {
                            equalTo: '#Password'
                        },
                        PhoneNumber: 'required'
                    },
                    messages: {
                        Name: 'Please enter your name',
                        Email: 'Please enter a valid email address',
                        Password: {
                            required: 'Please provide a password',
                            minlength: 'The password atleast 6 characters long'
                        },
                        PhoneNumber: 'Please enter a mobile number'
                    },
                    submitHandler: function (form) {
                        var formAddUser = $(form);

                        if (!formAddUser.valid()) return;

                        formAddUser.submit(addUserAsync(event));
                    }
                });
            });
            controls.appModal.modal('show');
        });

        controls.appModal.on('shown.bs.modal', function () {
            if ($('#Name')) {
                $('#Name').focus();
            }
        });

        controls.appModal.on('hidden.bs.modal', function () {
            //remove existing button in modal-footer
            //that has been created dynamically
            $(this).find('.modal-footer').empty();
        });

        controls.appModalContent.on('click', '#btnCloseDialog', function () {
            controls.appModal.modal('hide');
            location.reload();
        });

        controls.btnEditUserDialog.on('click', function (e) {
            var btn = $(e.currentTarget),
                userID = btn.data('userid');

            createModalDialog({
                modalTitle: 'Edit user',
                modalSize: ''
            });

            var appModalBody = controls.appModal.find('.modal-body');

            appModalBody.append('<p class="text-center"><i class="fa fa-spinner fa-pulse fa-2x"></i></p>');

            appModalBody.load('/admin/edituserpartialview?userID=' + userID, function () {

                //add frmUpdateUser form in the controls object
                controls.frmUpdateUser = controls.appModalContent.find('#frmUpdateUser');

                controls.frmUpdateUser.validate({
                    rules: {
                        Name: 'required',
                        PhoneNumber: 'required'
                    },
                    messages: {
                        Name: 'Please enter a name',
                        PhoneNumber: 'Please enter a mobile number'
                    },
                    submitHandler: function (form) {
                        var formUpdateUser = $(form);

                        if (!formUpdateUser.valid()) return;

                        formUpdateUser.submit(updateUserAsync(event));
                    }
                });
            });

            controls.appModal.modal('show');
        });

        controls.appModalContent.on('submit', '#frmAddUserToRole', function (e) {
            e.preventDefault();

            var form = $(this);

            $.ajax({
                method: 'post',
                url: '/admin/addusertorole',
                data: form.serialize(),
                success: function (response) {
                    if (response.isSuccess) {
                        form.find('#addToRoleStatus').text(response.message)
                            .addClass('text-success')
                            .removeClass('text-danger');

                        //append the roles dynamically
                        controls.appModalContent.find('#tblRoles')
                            .append('<tr><td><button type="button" class="btn btn-danger btn-xs btnRemoveRole" data-userid="' + response.userID + '" data-rolename="' + response.roleName + '">Remove</button></td>' +
                            '<td>' + response.roleName + '</td></tr>');

                    } else {
                        $.each(response.errors, function (key, value) {
                            form.find('#addToRoleStatus').text(value)
                                .addClass('text-danger')
                                .removeClass('text-success');
                        });
                    }
                },
                error: function (jqXHR) {
                    form.find('#addToRolesStatus').text(jqXHR.responseText)
                        .addClass('text-danger')
                        .removeClass('text-success');
                }
            });

        });

        controls.appModalContent.on('click', '.btnRemoveRole', function (e) {
            var btn = $(e.currentTarget),
                userID = btn.data('userid'),
                roleName = btn.data('rolename');

            $.ajax({
                method: 'post',
                url: '/admin/removefromrole',
                contentType: 'application/json',
                data: JSON.stringify({
                    userID: userID,
                    roleName: roleName
                }),
                success: function (response) {
                    if (response.isSuccess) {
                        controls.appModalContent.find('#removeFromRoleStatus').text(response.message)
                            .addClass('text-success')
                            .removeClass('text-danger');

                        btn.closest('tr').fadeOut();

                    } else {
                        $.each(response.errors, function (key, value) {
                            controls.appModalContent.find('#removeFromRoleStatus').text(value)
                                .addClass('text-danger')
                                .removeClass('text-success');
                        });
                    }
                },
                error: function (jqXHR) {

                }
            });
        });
        controls.btnResetPasswordDialog.on('click', function (event) {
            var btn = $(event.currentTarget),
                email = btn.data('email');

            createModalDialog({
                modalTitle: 'Reset password',
                modalSize: '<button id="btnCloseDialog" type="button" class="btn btn-default">Close</button>'
            })

            var appModalBody = controls.appModal.find('.modal-body');

            appModalBody.append('<p class="text-center"><i class="fa fa-spinner fa-pulse fa-2x"></i></p>');

            appModalBody.load('/admin/resetpasswordpartialview?email=' + email);

            controls.appModal.modal('show');
        });

        controls.appModalContent.on('submit', '#frmResetPassword', function (e) {
            e.preventDefault();

            var form = $(this),
                alertDialog;

            toggleEnableButton({
                button: '#btnResetPassword',
                innerHTML: 'Resetting the password... &nbsp; <i class="fa fa-spinner fa-pulse"></i>'
            });

            alertDialog = createAlertDialog();

            $.ajax({
                method: 'post',
                url: '/admin/resetpassword',
                data: form.serialize(),
                success: function (response) {
                    alertDialog.addClass('alert-success')
                        .append('<p class="text-center">' + response.message + '</p>');
                    controls.appModalContent.find('.modal-body').prepend(alertDialog);

                    toggleEnableButton({
                        button: '#btnResetPassword',
                        innerHTML: 'Submit',
                        disabled: false
                    });
                },
                error: function (jqXHR) {
                    alertDialog.addClass('alert-danger')
                        .append('<p class="text-center">' + jqXHR.responseText + '</p>');
                    controls.appModalContent.find('.modal-body').prepend(alertDialog);
                }
            })
        });

        controls.btnDeleteUserDialog.on('click', function () {
            var btn = $(event.currentTarget),
                email = btn.data('email');

            createModalDialog({
                modalTitle: 'Delete user',
                modalSize: 'modal-sm'
            });

            var appModalBody = controls.appModal.find('.modal-body');

            appModalBody.append('<p class="text-center"><i class="fa fa-spinner fa-pulse fa-2x"></i></p>');

            appModalBody.load('/admin/deleteuserpartialview?email=' + email);

            controls.appModal.modal('show');
        });

        controls.appModalContent.on('submit', '#frmDeleteUser', function (e) {
            e.preventDefault();

            var form = $(this),
                alertDialog;

            toggleEnableButton({
                button: '#btnDeleteUser',
                innerHTML: 'Deleting user... <i class="fa fa-spinner fa-pulse"></i>',
                disabled: true
            });

            alertDialog = createAlertDialog();

            $.ajax({
                method: 'post',
                url: '/admin/deleteuser',
                data: form.serialize(),
                success: function (response) {
                    if (response.isSuccess) {
                        alertDialog.addClass('alert-success');
                    } else {
                        alertDialog.addClass('alert-danger');
                    }

                    alertDialog.append('<p class="text-center">' + response.message + '</p>');

                    controls.appModal.find('.modal-body').empty()
                        .prepend(alertDialog);

                    toggleEnableButton({
                        button: '#btnDeleteUser',
                        innerHTML: 'Submit',
                        disabled: false
                    });
                },
                error: function (jqXHR) {
                    alertDialog.addClass('alert-danger')
                        .append('<p class="text-center">' + response.responseText + '</p>');

                    controls.appModal.find('.modal-body').prepend(alertDialog);

                    toggleEnableButton({
                        button: '#btnDeleteUser',
                        innerHTML: 'Submit',
                        disabled: false
                    });
                }
            });

        });
    };


})(jQuery, window.UserWidget = window.UserWidget || {});

UserWidget.init();