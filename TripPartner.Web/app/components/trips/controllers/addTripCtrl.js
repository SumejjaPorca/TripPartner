(function (tripModule) {

    tripModule.controller('addTripCtrl', ['$scope', 'TripManager', '$state', 'AccountManager', 'GeolocationService', function ($scope, mngr, $state, accMngr, geoService) {


        var init = function () {

            $scope.ok = true;
            $scope.message = '';
            var user = accMngr.getCurrentUser();
            if (accMngr.getCurrentUser.IsLoggedIn == false) {
                $scope.message = 'You must be logged in first to add a new trip.';
                $scope.ok = false;
            }
            var today = new Date();
            $scope.newTrip = {
                DateStarted: today,
                DateEnded: today,
                CreatorId: accMngr.getCurrentUser.Id,
                CreatorUsername: accMngr.getCurrentUser.Username,
                Destination: { Address: 'Sarajevo', Lat: 43.85, Long: 18.25 },
                Origin: { Address: 'Travnik', Lat: 44.222321, Long: 17.6651 }
            };


          
            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.

          }

        $scope.onloadfunc = function () {

            var mapProp = {
                center: { Lat: 43.9000, Lng: 17.4 },
                zoom: 7
            };
            $scope.map = geoService.instantiateMap("map", mapProp);
            $scope.destMarker = new geoService.Marker($scope.map, { lat: $scope.newTrip.Destination.Lat, lng: $scope.newTrip.Destination.Long });
            $scope.originMarker = new geoService.Marker($scope.map, { lat: $scope.newTrip.Origin.Lat, lng: $scope.newTrip.Origin.Long });

        }

        init();

        $scope.DestinationChanged = function () {
            geoService.getLatLng($scope.newTrip.Destination.Address).then(
                function (response) {
                    $scope.destMarker.changePosition(response);
                    $scope.newTrip.Destination.Lat = response.H;
                    $scope.newTrip.Destination.Long = response.L;

                    $scope.map.panTo($scope.destMarker.getPosition());
            });
        }
        $scope.OriginChanged = function () {
            geoService.getLatLng($scope.newTrip.Origin.Address).then(
                function (response) {
                    $scope.originMarker.changePosition(response);
                    $scope.newTrip.Origin.Lat = response.H;
                    $scope.newTrip.Origin.Long = response.L;
                    $scope.map.panTo($scope.originMarker.getPosition());
                });
        }

        $scope.submit = function () {
            mngr.AddNew($scope.newTrip).then(
                function (response) {
                    $scope.message = "Trip is successfully added to your collection of trips."
                }, function (response) {
                    $scope.ok = false;
                    $scope.message = response.data;
                });
        
        }

    }]);

}(angular.module("app.trips")));