

function getSelectedCategories() {
    var res = [];
    $(".checkCategoryInput").each(function (i) {
        if (this.checked) {
            res.push(this.id.split("_")[1]);
        }        
    })
    return res.join(";");
}

function getSelectedCharVals() {
    var res = [];
    $(".charvalRadioInput").each(function (i) {
        if (this.checked) {
            res.push(this.id.split("_")[1] + "," + this.id.split("_")[2]);
        }
    })
    return res.join(";");
}

function sendData() {
    var model = {
        ModelNamePattern: $("#ModelNamePattern").val(),
        DescriptionPattern: "",
        SelectedCategoriesIds: getSelectedCategories(),
        SelectedCharVals: getSelectedCharVals()
    };
    console.log(model);
    $("#SelectedCategoriesIds").val(getSelectedCategories());
    $("#SelectedCharVals").val(getSelectedCharVals());
    
}


$("#searchForm").submit(function (event) {
    console.log("Handler for .submit() called.");
    sendData();
    
});
$("#checkAllCategories").change(function () {
    var _checked = this.checked;
    $(".checkCategoryInput").each(function (i) {
        this.checked = _checked;
    })
})
