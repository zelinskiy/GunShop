

function getSelected() {
    var res = [];
    $(".selectedCommodityCheckbox").each(function (i) {
        if (this.checked) {
            res.push(this.id.split("_")[1]);
        }
    })
    return res.join(";");
}


function sendData() {
    var model = {
        StorageBId: $("#StorageBId").val(),
        Id: $("#Id").val(),
        SelectedCommoditiesIds: $.makeArray(getSelected())
    };
    console.log(model);
    $.post({
        url: "/Location/Storage",
        data: model,
        success: function (d) {
            console.log(d);
            window.location.replace("/Location/Storage?storageId=" + $("#StorageBId").val());
        }
    });
}

$("#moveForm").submit(function (event) {
    console.log("Handler for .submit() called.");
    sendData();
    event.preventDefault();
});

$("#selectAllCommodities").change(function () {
    var _checked = this.checked;
    $(".selectedCommodityCheckbox").each(function (i) {
        this.checked = _checked;
    })
})