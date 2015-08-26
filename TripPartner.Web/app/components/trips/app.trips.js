(function () {
    'use strict';
    var app = angular.module('app.trips', ['ui.router', 'app.account'])
                     .config(['$stateProvider', '$httpProvider', function ($stateProvider, $httpProvider) {
                        
                         $stateProvider
                    .state('trips', {
                        url: '/trips',
                        abstract:true,
                        templateUrl: '/app/components/trips/partials/main.html',
                        controller: 'tripsCtrl'
                    })
                        .state('trips.add', {
                        url: '/add',
                        templateUrl: '/app/components/trips/partials/add.html',
                        controller: 'addTripCtrl'
                    })

                         .state('trips.byLoc', {
                             url: '/byLoc/?locId?serial',
                             templateUrl: '/app/components/trips/partials/tripsByLoc.html',
                             controller: 'byLocCtrl',
                             params: { locId: undefined, serial: undefined },
                             resolve: {
                                 id: ['$stateParams', function ($stateParams) {
                                     return $stateParams.locId;
                                 }]
                             }
                         })
                          .state('trips.all', {
                              url: '/all?serial',
                              templateUrl: '/app/components/trips/partials/all.html',
                              controller: 'allTripsCtrl',
                              params: {serial: undefined}
                             })
                         .state('trips.byUser', {
                             url: '/byUser/{userId}?serial',
                             templateUrl: '/app/components/trips/partials/grid.html',
                             controller: 'byUserCtrl',
                             params: { userId: undefined, serial: undefined },
                             resolve: {
                                 id: ['$stateParams', function ($stateParams) {
                                     return $stateParams.userId;
                                 }]
                             }
                         })
                             .state('trips.details', {
                                 url: '/details/{tripId:int}',
                                 templateUrl: '/app/components/trips/partials/trip-detail.html',
                                 controller: 'tripDetailCtrl',
                                 params: {tripId: null}
                             });
                     }
                     ])
                     .controller('tripsCtrl', ['$scope', function ($scope) {
                         $scope.onloadfunc = function () { }
                     }])
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
                                trips: '=',
                                serial: '='
                            },
                            templateUrl: '/app/components/trips/directives/trips-grid.html',
                            controller: ['$scope', function ($scope) {
                                $scope.isNumber = angular.isNumber($scope.serial) && $scope.serial >= 0;
                                $scope.show = function (index) {
                                    $scope.serial = index;
                                    $scope.isNumber = angular.isNumber($scope.serial) && $scope.serial >= 0;
                                }
                            }]
                        }
                    })
                    .directive('tripsList', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                serial: '=',
                                trips: '='
                            },
                            templateUrl: '/app/components/trips/directives/trips-list.html',
                            controller: ['$scope', function ($scope) {
                                $scope.Next = function () {
                                    $scope.serial = $scope.serial + 1;
                                    return true;
                                }
                                $scope.HasNext = function () {
                                    return $scope.serial < $scope.trips.length - 1;
                                }

                                $scope.HasPrevious = function () {
                                    return $scope.serial > 0;
                                }
                                $scope.Previous = function () {
                                    $scope.serial = $scope.serial - 1;
                                    return true;
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