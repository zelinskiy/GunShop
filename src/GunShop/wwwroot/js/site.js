
angular.module('my-app', [], function () {

}).controller('TestController', TestController)

function TestController($scope) {
    $scope.list = [{ value: 'value' }, { value: 'value' }, { value: 'value' }];
}