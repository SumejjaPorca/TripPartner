(function (tripModule) {

    tripModule.controller('allTripsCtrl', ['$scope', 'TripManager', 'AccountManager', '$stateParams', function ($scope, mngr, accMngr, $stateParams) {

        $scope.Trips = [];

        var init = function () {

            $scope.message = "";

            $scope.Serial = $stateParams.serial;
            if ($scope.Serial == undefined)
                $scope.Serial = -1;

            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
           
            $scope.accMngr = accMngr;

            mngr.getAll().then(
                  function (response) {
                      angular.copy(response, $scope.Trips);
                      $scope.Title = "Trips";
                  }, function (response) {
                      $scope.message = "Trips were not found."
                  });
        }


        init();



    }]);

}(angular.module("app.trips")));