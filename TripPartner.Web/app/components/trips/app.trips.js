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
                             url: '/byLoc/{locId:int}',
                             templateUrl: '/app/components/trips/partials/grid.html',
                             controller: 'byLocCtrl',
                             params: { locId: undefined },
                             resolve: {
                                 id: ['$stateParams', function ($stateParams) {
                                     return $stateParams.locId;
                                 }]
                             }
                         })
                         .state('trips.byUser', {
                             url: '/byUser/{userId}',
                             templateUrl: '/app/components/trips/partials/grid.html',
                             controller: 'byUserCtrl',
                             params: { userId: undefined },
                             resolve: {
                                 id: ['$stateParams', function ($stateParams) {
                                     return $stateParams.userId;
                                 }]
                             }
                         })
                        .state('trips.byLoc.details', {
                            url: '/details/{serial:int}',
                            templateUrl: '/app/components/trips/partials/list.html',
                            controller: 'tripsByLocListCtrl',
                            params: { trips: null, serial: null }
                        })
                        .state('trips.byUser.details', {
                            url: '/details/{serial:int}',
                            templateUrl: '/app/components/trips/partials/list.html',
                            controller: 'tripsByUserListCtrl',
                            params: { trips: null, serial: null }
                        })

                             .state('trips.details', {
                                 url: '/details/{tripId:int}',
                                 templateUrl: '/app/components/trips/partials/trip-detail.html',
                                 controller: 'tripDetailCtrl',
                                 params: {tripId: null}
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
                                trips: '=',
                                title: '='
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
                                serial: '=',
                                trips: '=',
                                title: '=',
                                mngr: '='
                            },
                            templateUrl: '/app/components/trips/directives/trips-list.html',
                            controller: ['$scope', function ($scope) {
                                $scope.Next = function () {
                                    $scope.serial = $scope.serial + 1;
                                    mngr.getDetailed(trips[serial].Id).then(function (response) {
                                        trips[serial] = response.data;
                                    });
                                }
                                $scope.Previous = function () {
                                    $scope.serial = $scope.serial - 1;
                                    mngr.getDetailed(trips[serial].Id).then(function (response) {
                                        trips[serial] = response.data;
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