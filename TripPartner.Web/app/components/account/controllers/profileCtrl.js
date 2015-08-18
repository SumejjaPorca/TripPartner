(function (account) {

    account.controller('profileCtrl', ['$scope', 'AccountManager', '$state', '$stateParams', function ($scope, mngr, $state, $stateParams) {

        if ($stateParams.own == true)
            $scope.User = mngr.getCurrentUser();
        //TO DO: ELSE


    }]);

}(angular.module("app.account")));