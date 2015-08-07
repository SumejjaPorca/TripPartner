(function (account) {

    account.controller('loginCtrl', ['$scope', 'AccountManager', '$state', function ($scope, mngr, $state) {

        $scope.loginModel = {};
        $scope.message = "";

        init();

        function init() {
            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
            $scope.loginModel = {username:"", password:""};
        }
        $scope.login = function () {
            mngr.Login($scope.loginModel).then(
                function (response) {
                   $state.go('home');
                }, function (response) {
                    $scope.message = response.data.error_description;
                });
        }


    }]);

}(angular.module("app.account")));