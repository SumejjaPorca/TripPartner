(function () {
    'use strict';
    var app = angular.module('app.trips', ['ui.router'])
                     .config(['$stateProvider', function ($stateProvider) {

                         $stateProvider //check how to send data with state - > we need to send  GET trips parameter
                    .state('trips', {
                        url: '/trips',
                        controller: 'tripsCtrl',
                        templateUrl: '/app/components/stories/partials/trips.html'
                    })

                         ;
                     }
                     ])
                     .run(function () {
                     })
                    .controller("tripsCtrl",[ '$scope', 'TripManager', function ($scope, mngr) {
                        $scope.Serial = 0;

                        var init = function () {
                            mngr.GetTrips({param: ""}).then(function (response) {
                                $scope.Trips = response.data.trips; //TO DO: this
                                if ($scope.Trips != undefined)
                                    $scope.Serial = $scope.Trips.Length / 2;
                                else
                                    $scope.Trips = [];
                            });


                        };

                        init();

                        $scope.Details = function(index){
                            //TO DO: change state to trip and send trip to that state
                        }
                    }])
                    .directive('smallTrip', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                trip: '='
                            },
                            templateUrl: '/app/components/trips/directives/small-trip.html'
                           
                            
                        }
                    })
                    .directive('tripDirective', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                Serial: '=',
                                Trips: '='
                            },
                            templateUrl: '/app/components/trips/directives/trip-directive.html',
                            controller: function ($scope) {
                                $scope.Next = function () {
                                    $scope.Serial = $scope.Serial + 1;
                                }
                                $scope.Previous = function () {
                                    $scope.Serial = $scope.Serial - 1;
                                }
                            }
                        }

                    });

})();