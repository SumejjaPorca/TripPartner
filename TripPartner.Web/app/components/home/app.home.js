(function () {
    'use strict';
    var app = angular.module('app.home', ['ui.router', 'app.account', 'app.stories'])
                     .config(['$stateProvider', '$httpProvider', function ($stateProvider, $httpProvider) {

                        

                         $stateProvider
                    .state('home', {
                        url: '/home',
                        controller: 'homeCtrl',
                        templateUrl: '/app/components/home/partials/home.html'
                    });
                     }
                     ])
                     .run(function () {
                        
                     })
                    .controller("homeCtrl", ['$scope', 'StoryManager', function ($scope, mngr) {
                        $scope.Serial = 0;
                        $scope.Stories = [];
                        var init = function () {
                            mngr.getTopStories().then(function (response) {
                                angular.copy(response, $scope.Stories);
                                if ($scope.Stories != undefined)
                                    $scope.Serial = Math.floor($scope.Stories.length / 2);
                                else
                                    angular.copy([], $scope.Stories);
                            });
                        };
                        init();
                    }]);

})();