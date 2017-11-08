(function($, widgetObject){
    var controls = {
        frmAddProduct : $('#frmAddProduct'),
        txtProductNo: $('#ProductNo'),
        txtProductName: $('#ProductName'),
        txtProductDescription: $('#ProductDescription'),
        txtUnitPrice: $('#UnitPrice'),
        txtQoh: $('#Qoh'),
        txtImage: $('#Image'),
        txtProductCategoryID: $('#txtProductCategoryID')
    };

    widgetObject.init = function () {
        bindNewProductUI();
    };

    var bindNewProductUI = function () {        

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
                PostedImage: {
                    required: true,
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
                    required: 'Image is required',
                    extension: 'File must be jpg or jpeg'
                },
                ProductCategoryID: 'Please select a category'
            },
            submitHandler: function (form) {                
                form.submit();
            }
        });
    };

})(jQuery, window.NewProductWidget = window.NewProductWidget || {});

NewProductWidget.init();