(function (tripModule) {

    tripModule.controller('tripDetailCtrl', ['$scope', 'TripManager', '$state', '$stateParams', function ($scope, mngr, $state, $stateParams) {


        init();

        var init = function () {
            $scope.message = "";
            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
           
            mngr.getById($stateParams.tripId).then(
                  function (response) {
                      $scope.Trip = response;
                    }, function (response) {
                      $scope.message = "Trip with id: " + $stateParams.tripId + "was not found."
                  });
        }


    }]);

}(angular.module("app.trips")));