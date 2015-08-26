(function () {
    'use strict';
    var app = angular.module('app.stories', ['ui.router', 'app.account', 'app.trips'])
                     .config(['$stateProvider', '$httpProvider', function ($stateProvider, $httpProvider) {
                         $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

                         $stateProvider
                    .state('stories', {
                        url: '/stories',
                        abstract:true,
                        controller: 'storiesCtrl',
                        templateUrl: '/app/components/stories/partials/stories.html'
                    })
                     .state('stories.all', {
                         url: '/all?serial',
                         templateUrl: '/app/components/stories/partials/grid.html',
                         controller: 'allStoriesCtrl',
                         params:{ serial: undefined},
                         resolve: {
                             index: function () {
                                 return '';
                             }
                         }
                         
                     })
                     .state('stories.add', {
                         url: '/add/{tripId}',
                         templateUrl: '/app/components/stories/partials/add.html',
                         controller: 'addStoryCtrl',
                         params: { tripId: null}
                     })
                     .state('stories.byUser', {
                         url: '/byUser/{userId}?serial',
                         templateUrl: '/app/components/stories/partials/grid.html',
                         controller: 'storiesByUserCtrl',
                         params: { userId: null, serial: undefined }
                     })
                     .state('stories.byTrip', {
                         url: '/byTrip/{tripId}?serial',
                         templateUrl: '/app/components/stories/partials/grid.html',
                         controller: 'storiesByTripCtrl',
                         params: { userId: null, serial: undefined }
                     });
                    }])
                     .run(function () {
                             })
                    .controller("storiesCtrl",['$scope', 'StoryManager', function ($scope, mngr) {

                    }]).directive('storyDirective', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                serial: '=',
                                stories: '='
                            },
                            templateUrl: '/app/components/stories/directives/story-directive.html',
                            controller: function ($scope) {
                               $scope.Next = function () {
                                   $scope.serial = $scope.serial + 1;
                                   return true;
                               }

                               $scope.HasNext = function () {
                                   if ($scope.stories != undefined)
                                       return $scope.serial < $scope.stories.length - 1;
                                   else return false;
                               }

                               $scope.HasPrevious = function () {
                                   return $scope.serial > 0;
                               }

                                $scope.Previous = function () {
                                    $scope.serial = $scope.serial - 1;
                                    return true;
                                }
                            }

                        };
                    }).directive('smallStory', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                story: '='
                            },
                            templateUrl: '/app/components/stories/directives/small-story.html'
                           
                        };
                    }).directive('storiesGrid', function () {
                        return {
                            restrict: 'E',
                            scope: {
                                stories: '=',
                                serial: '='
                            },
                            templateUrl: '/app/components/stories/directives/stories-grid.html',
                            controller: ['$scope', function ($scope) {
                                $scope.isNumber = angular.isNumber($scope.serial) && $scope.serial >= 0;
                                $scope.show = function (i) {
                                    $scope.serial = i;
                                    $scope.isNumber = angular.isNumber($scope.serial) && $scope.serial >= 0;
                                }
                            }]

                        };
                    });

})();