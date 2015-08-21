(function (tripModule) {

    tripModule.controller('tripsByUserListCtrl', ['$scope', 'TripManager', 'AccountManager', '$state', '$stateParams', 'id', function ($scope, mngr, accMngr, $state, $stateParams, id) {



        var init = function () {
            $scope.message = "";
            var user = {username: "", email: "", id: ""};
            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
            if ($stateParams.trips == null) {
                accMngr.getUserInfo(id).then(
                function (response) {
                    user.username = response.Username;
                    user.email = response.Email;
                    user.userId = id;
                    mngr.getByUserId(id).then(
                 function (response) {
                     $scope.Trips = response;
                     if ($stateParams.serial == null)
                         $scope.Serial = $scope.Trips.length / 2;
                     else if ($stateParams.serial >= $scope.Trips.length || $stateParams.serial < 0)
                         $scope.message = "Serial number for the trip is invalid.";
                 }, function (response) {
                     $scope.message = "Trips by user: " + user.username + "were not found."
                 });
                },
                function (response) {
                    $scope.message = "Sorry, location with id: " + id + " was not found.";
                });

            }
            else if ($stateParams.serial == null) {
                $scope.Trips = $stateParams.trips;
                $scope.Serial = $scope.Trips.length / 2;
            }
            else {
                $scope.Trips = $stateParams.trips;
                if ($stateParams.serial >= $scope.Trips.length || $stateParams.serial < 0)
                    $scope.message = "Serial number for the trip is invalid.";
                else
                    $scope.Serial = $stateParams.serial;
            }
        }

        init();

    }]);

}(angular.module("app.trips")));