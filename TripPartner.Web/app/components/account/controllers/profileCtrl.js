(function (account) {

    account.controller('profileCtrl', ['$scope', 'AccountManager', '$state', '$stateParams', function ($scope, mngr, $state, $stateParams) {

        $scope.message = "";


        var init = function () {
            if ($stateParams.own == 'true')
                $scope.User = mngr.getCurrentUser();
            else {
                mngr.getUserInfo($stateParams.userId).then(
                    function (response) {
                        $scope.User = response;
                    }, function (response) {
                        $scope.message = "User with id: " + $stateParams.userId + " was not found.";
                    });
            }
        }


        init();
      

    }]);

}(angular.module("app.account")));