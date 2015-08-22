(function (tripModule) {

    tripModule.controller('allTripsCtrl', ['$scope', 'TripManager', '$state', function ($scope, mngr,  $state) {


        var init = function () {

            $scope.message = "";

            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
           
            mngr.getAll().then(
                  function (response) {
                      $scope.Trips = response;
                      $scope.Title = "Trips";
                  }, function (response) {
                      $scope.message = "Trips were not found."
                  });
        }


        init();



    }]);

}(angular.module("app.trips")));