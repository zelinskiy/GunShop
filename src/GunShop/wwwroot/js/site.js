
angular.module('my-app', [], function () { })
    .controller('TestController', TestController)


function TestController($scope) {
    $scope.charsList = [];

    $scope.extendDown = function (e) {
        if (e.keyCode == 13) {
            console.log(e.delegateTarget.value);
            $scope.charsList.push({ name: e.delegateTarget.value });
            e.delegateTarget.value = "";
        }        
    }

    $scope.chk = function () { console.log($scope.charsList); }
}

var textInputtedEvent = new CustomEvent("textInputtedEvent", {});

$(document).ready(function () {
    
})