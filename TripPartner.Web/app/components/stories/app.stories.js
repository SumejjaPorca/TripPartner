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
                         url: '/all',
                         templateUrl: '/app/components/stories/partials/all.html',
                         controller: 'allStoriesCtrl',
                         resolve: {
                             index: function () {
                                 return '';
                             }
                         }
                         
                     })
                     .state('stories.all.details', {
                         url: '/details/{serial}',
                         templateUrl: '/app/components/stories/partials/story-list.html',
                         controller: 'listCtrl',
                         params: { stories: null, serial: null }
                     })
                         ;
                     }
                     ])
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
                                }
                                $scope.Previous = function () {
                                   $scope.serial = $scope.serial - 1;
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
                                stories: '='
                            },
                            templateUrl: '/app/components/stories/directives/stories-grid.html'

                        };
                    });

})();