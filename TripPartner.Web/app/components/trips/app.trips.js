(function () {
    'use strict';
    var app = angular.module('app.trips', ['ui.router'])
                     .config(['$stateProvider', function ($stateProvider) {

                         $stateProvider
                    .state('trips', {
                        abstract: true,
                        url: '/trips',
                        templateUrl: '/app/components/trips/partials/main.html'
                    })
                         .state('trips.byLoc', {
                             url: '/byLoc',
                             templateUrl: '/app/components/trips/partials/list.html',
                             controller: 'byLocCtrl',
                             params: {locId: undefined}
                         })
                         .state('trips.byUser', {
                             url: '/byUser',
                             templateUrl: '/app/components/trips/partials/list.html',
                             controller: 'byUserCtrl',
                             params: {userId: undefined}
                         })
                        .state('trips.byLoc.details', {
                            url: '/details',
                            templateUrl: '/app/components/trips/partials/trips-detail.html',
                            controller: 'tripsDetailCtrl',
                            params: { Trips: [], Serial: 0 }
                        })
                        .state('trips.byUser.details', {
                            url: '/details',
                            templateUrl: '/app/components/trips/partials/trips-detail.html',
                            controller: 'tripsDetailCtrl',
                            params: { Trips: [], Serial: 0 }
                        })

                             .state('trips.details', {
                                 url: '/details',
                                 templateUrl: '/app/components/trips/partials/trip-detail.html',
                                 controller: 'tripDetailCtrl',
                                 params: {TripId: undefined}
                             });
                     }
                     ])
                     .run(function () {
                     })
                    .directive('smallTrip', function () { 
                      return {
                            restrict: 'E',
                            scope: {
                                trip: '='
                            },
                            templateUrl: '/app/components/trips/directives/small-trip.html'            
                            
                        }
                    })
                    .directive('tripsGrid', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                Trips: '='
                            },
                            templateUrl: '/app/components/trips/directives/trips-grid.html',
                            controller: ['$scope', function ($scope) {
                               
                            }]
                        }
                    })
                    .directive('tripsList', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                Serial: '=',
                                Trips: '=',
                                Title: '=',
                                mngr: '='
                            },
                            templateUrl: '/app/components/trips/directives/trips-list.html',
                            controller: ['$scope', function ($scope) {
                                $scope.Next = function () {
                                    $scope.Serial = $scope.Serial + 1;
                                    mngr.getDetailed(Trips[Serial].Id).then(function (response) {
                                        Trips[Serial] = response.data;
                                    });
                                }
                                $scope.Previous = function () {
                                    $scope.Serial = $scope.Serial - 1;
                                    mngr.getDetailed(Trips[Serial].Id).then(function (response) {
                                        Trips[Serial] = response.data;
                                    });
                                }
                            }]
                        }

                    })
                    .directive('tripDetail', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                trip: '='
                            },
                            templateUrl: '/app/components/trips/directives/trip-detail.html',
                            controller: ['$scope', function ($scope) {
                               
                                }]
                        }

                    });

})();