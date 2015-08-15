(function () {
    'use strict';
    var app = angular.module('app.stories', ['ui.router', 'app.account', 'app.trips'])
                     .config(['$stateProvider', '$httpProvider', function ($stateProvider, $httpProvider) {
                         $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

                         $stateProvider
                    .state('stories', {
                        url: '/stories',
                        controller: 'storiesCtrl',
                        templateUrl: '/app/components/stories/partials/stories.html'
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
                                Serial: '=',
                                Stories: '='
                            },
                            templateUrl: '/app/components/stories/directives/story-directive.html',
                            controller: function ($scope) {
                               $scope.Next = function () {
                                    $scope.Serial = $scope.Serial + 1;
                                }
                                $scope.Previous = function () {
                                   $scope.Serial = $scope.Serial - 1;
                                }
                            }

                        };
                    });

})();