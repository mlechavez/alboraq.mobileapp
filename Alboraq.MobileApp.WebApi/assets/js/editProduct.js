(function ($, widgetObject) {
    var controls = {
        frmAddProduct: $('#frmUpdateProduct'),
        txtProductNo: $('#ProductNo'),
        txtProductName: $('#ProductName'),
        txtProductDescription: $('#ProductDescription'),
        txtUnitPrice: $('#UnitPrice'),
        txtQoh: $('#Qoh'),
        txtImage: $('#Image'),
        txtProductCategoryID: $('#txtProductCategoryID'),
        imgPreviewer: $('#imgPreviewer')
    };

    widgetObject.init = function () {
        bindEditProductUI();
    };

    var bindEditProductUI = function () {

        controls.frmAddProduct.validate({
            rules: {
                ProductNo: 'required',
                ProductName: 'required',
                ProductDescription: 'required',
                UnitPrice: {
                    required: true,
                    number: true,
                    min: 0.00
                },
                Qoh: {
                    required: true,
                    number: true,
                    min: 0
                },
                Image: {                    
                    extension: 'jpg|jpeg'
                },
                ProductCategoryID: 'required'
            },
            messages: {
                ProductNo: 'Please enter a product no',
                ProductName: 'Please enter a product name',
                ProductDescription: 'Please enter a description',
                UnitPrice: {
                    required: 'Please enter a valid unit price'
                },
                Qoh: {
                    required: 'Please enter a valid Quantity'
                },
                Image: {                    
                    extension: 'File must be jpg or jpeg'
                },
                ProductCategoryID: 'Please select a category'
            },
            submitHandler: function (form) {
                form.submit();
            }
        });

        controls.txtImage.on('change', function () {
            var image = this;
            
            if (image.files && image.files[0]) {                
                
                var imageDir = new FileReader();

                imageDir.onload = function (e) {                    
                    controls.imgPreviewer.attr('src', e.target.result);
                }

                imageDir.readAsDataURL(image.files[0]);
            }
        })
    };

})(jQuery, window.EditProductWidget = window.EditProductWidget || {});

EditProductWidget.init();