
function getData() {
    var model = new Object();
    model.MasterCategoryId = $("#MasterCategoryId").val();
    model.Name = $("#Name").val();
    model.Characteristics = angular
        .element("#extendableInputsList")
        .scope()
        .charsList
        .map(function(c) {
            return {
                Name: c.name,
                Type: "custom",
                AvailableValues: c.possvals
            }
        });
    
    console.log(model);
    return model;

}

function sendData() {
    $.post({
        url: "/Category/AddCategory",
        data: getData(),
        success: function (d) {
            console.log(d);
        }
    });
}

angular.module('my-app', [], function() {})
    .controller('TestController', TestController);


function TestController($scope) {
    $scope.charsList = [];

    $scope.extendDown = function (e) {
        if (e.keyCode == 13) {
            console.log(e.delegateTarget.value);
            $scope.charsList.push({
                name: e.delegateTarget.value,
                possvals: ""
        });
            e.delegateTarget.value = "";
        }        
    }

    $scope.chk = function () { console.log($scope.charsList); }
}

var textInputtedEvent = new CustomEvent("textInputtedEvent", {});

$(document)
    .ready(function() {

        $("#submitButton").click(sendData);
    });