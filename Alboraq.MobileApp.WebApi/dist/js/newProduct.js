!function(e,t){var r={frmAddProduct:e("#frmAddProduct"),txtProductNo:e("#ProductNo"),txtProductName:e("#ProductName"),txtProductDescription:e("#ProductDescription"),txtUnitPrice:e("#UnitPrice"),txtQoh:e("#Qoh"),txtImage:e("#Image"),txtProductCategoryID:e("#txtProductCategoryID")};t.init=function(){i()};var i=function(){r.frmAddProduct.validate({rules:{ProductNo:"required",ProductName:"required",ProductDescription:"required",UnitPrice:{required:!0,number:!0,min:0},Qoh:{required:!0,number:!0,min:0},Image:{required:!0,extension:"jpg|jpeg"},ProductCategoryID:"required"},messages:{ProductNo:"Please enter a product no",ProductName:"Please enter a product name",ProductDescription:"Please enter a description",UnitPrice:{required:"Please enter a valid unit price"},Qoh:{required:"Please enter a valid Quantity"},Image:{required:"Image is required",extension:"File must be jpg or jpeg"},ProductCategoryID:"Please select a category"},submitHandler:function(e){e.submit()}})}}(jQuery,window.NewProductWidget=window.NewProductWidget||{}),NewProductWidget.init();