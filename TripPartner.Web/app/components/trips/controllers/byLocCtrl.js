(function (tripModule) {

    tripModule.controller('byLocCtrl', ['$scope', 'TripManager', 'LocationManager', '$state', '$stateParams', function ($scope, mngr, locMngr, $state, $stateParams) {

       
        init();

        var init = function () {
            $scope.message = "";
            var locationAddress = "";
            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
            locMngr.getById($stateParams.locId).then(
                function (response) {
                    locationAddress = response.Address;
                },
                function (response) {
                    $scope.message = "Sorry, location with id: " + $stateParams.locId + " was not found.";
                });
          mngr.getByLocId($stateParams.locId).then(
                function (response) {
                    $scope.Trips = response;
                    $scope.Title = "Trips around " + locationAddress + ":";
                }, function (response) {
                    $scope.message = "Trips around " + locationAddress + "were not found."
                });
        }


    }]);

}(angular.module("app.trips")));