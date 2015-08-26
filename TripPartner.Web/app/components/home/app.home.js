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
                                $scope.Stories = response;
                                if ($scope.Stories != undefined)
                                    $scope.Serial = $scope.Stories.length / 2;
                                else
                                    $scope.Stories = [];
                            });


                        };

                        init();
                    }]);

})();