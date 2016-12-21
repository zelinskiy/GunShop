


function uploadImage(formId, callback) {
    var data = new FormData($("#" + formId)[0]);

    $.ajax({
        url: "/File/AddFile",
        type: "POST",
        data: data,
        contentType: false,
        dataType: false,
        processData: false,
        success: callback
    });
}
