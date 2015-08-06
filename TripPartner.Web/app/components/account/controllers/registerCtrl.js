(function (account) {

    account.controller('registerCtrl',['$scope', 'AccountManager', function ($scope, mngr) {
       
        $scope.registerModel = {};

        init();

        function init() {
            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
            $scope.registerModel.email = "";
            $scope.registerModel.password = "";
            $scope.registerModel.confirmPassword = "";
        }

        $scope.register = function () {
            mngr.Register(registerModel).success(//TO DO: something
                );
        };

    }]);

}(angular.module("app.account")));