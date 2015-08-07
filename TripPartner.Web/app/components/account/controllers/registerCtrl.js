(function (account) {

    account.controller('registerCtrl',['$scope', 'AccountManager', '$timeout', '$state', function ($scope, mngr, $timeout, $state) {
       
        $scope.registerModel = {};
        $scope.message = "";
        $scope.success = false;

        init();

        function init() {
            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
            $scope.registerModel.email = "";
            $scope.registerModel.password = "";
            $scope.registerModel.confirmPassword = "";
        }

        $scope.register = function () {
            mngr.Register($scope.registerModel).then(
                function (response) {
                    $scope.success = true;
                    $scope.message = "You have successfully registered.";
                    changeState(2000, 'login');
                },
                function (response) {
                    $scope.success = false;
                    $scope.message = "Failed to register";

                }
                );
        };


        var changeState = function (millis, state) {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $state.go(state);
            }, millis);
        }

    }]);

}(angular.module("app.account")));