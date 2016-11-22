

function getCharVals() {
    var commodityTypeId = $("#CommodityTypeId").val();
    return $.makeArray($(".charval")
        .map(function (i) {
            return {
                CharacteristicId: $(this).attr("id").split("_")[1],
                Value: $(this).val(),
                CommodityTypeId: commodityTypeId
            }
        }));
}

function sendData() {
    var model = {
        CommodityTypeId: $("#CommodityTypeId").val(),
        CategoryId: $("#CategoryId").val(),
        CharacteristicsValues: getCharVals()
    };
    console.log(model);
    $.post({
        url: "/Category/AddCommodityTypeToCategory",
        data: model,
        success: function (d) {
            console.log(d);
            window.location.replace("/Commodity/AllCommodityTypes?categoryId=" + $("#CategoryId").val());
        }
    });
}

$(document)
    .ready(function () {
        $("#submitButton").click(sendData);

    });