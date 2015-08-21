(function (tripModule) {

    tripModule.controller('byUserCtrl', ['$scope', 'TripManager', 'AccountManager', '$state', '$stateParams', function ($scope, mngr, accMngr, $state, $stateParams) {



        var init = function () {
            $scope.message = "";
            var user = {username: "", email: "", userId: ""};

            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
            accMngr.getUserInfo($stateParams.userId).then(
                function (response) {
                    user.username = response.Username;
                    user.email = response.Email;
                    user.userId = $stateParams.userId;
                },
                function (response) {
                    $scope.message = "Sorry, user with id: " + $stateParams.userId + " was not found.";
                });
            mngr.getByUserId($stateParams.userId).then(
                  function (response) {
                      $scope.Trips = response;
                      $scope.Title = "Trips made by: " + user.username;
                  }, function (response) {
                      $scope.message = "Trips by " + user.username + "were not found."
                  });
        }

        init();


    }]);

}(angular.module("app.trips")));