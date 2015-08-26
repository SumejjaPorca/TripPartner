﻿(function (tripModule) {

    tripModule.controller('byLocCtrl', ['$scope', 'TripManager', 'LocationManager', '$stateParams', 'Location', 'GeolocationService', function ($scope, mngr, locMngr, $stateParams, Location, geoService) {

        $scope.Serial = $stateParams.serial;
        $scope.message = "";

        var init = function () {

            if ($scope.Serial == undefined)
                $scope.Serial = -1;
            $scope.Loc = new Location({Id:undefined, Address: 'Sarajevo', Lat: 43.85, Long: 18.25 });

            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.
            if ($stateParams.locId != null)
            {
                locMngr.getById($stateParams.locId).then(
                function (response) {
                    $scope.Loc = response;
                },
                function (response) {
                    $scope.Loc.Id = undefined;
                    $scope.message = "Sorry, location with id: " + $stateParams.locId + " was not found.";
                });
                mngr.getByLocId($stateParams.locId).then(
                      function (response) {
                          $scope.Trips = response;
                          $scope.Title = "Trips around " + $scope.Loc.Address + ":";
                      }, function (response) {
                          $scope.message = "Trips around " + $scope.Loc.Address + "were not found."
                      });
            }
            
        }

        init();

        $scope.onloadfunc = function () {

            var mapProp = {
                center: { Lat: 43.9000, Lng: 17.4 },
                zoom: 7
            };
            $scope.map = geoService.instantiateMap("map", mapProp);
            $scope.Marker = new geoService.Marker($scope.map, { lat: $scope.Loc.Lat, lng: $scope.Loc.Long });
          
        }

        $scope.LocationChanged = function () {
            geoService.getLatLng($scope.Loc.Address).then(
                function (response) {
                    $scope.Marker.changePosition(response);
                    $scope.Loc.Lat = response.G;
                    $scope.Loc.Long = response.K;
                   
                });
        }
        $scope.Find = function () {
            mngr.GetByLatLng($scope.Loc.Lat, $scope.Loc.Long).then(
                     function (response) {
                         $scope.Trips = response;
                         $scope.Title = "Trips around " + $scope.Loc.Address + ":";
                     }, function (response) {
                         $scope.Loc.Id = undefined;
                         $scope.message = "Trips around " + $scope.Loc.Address + "were not found."
                     });
        }


    }]);

}(angular.module("app.trips")));