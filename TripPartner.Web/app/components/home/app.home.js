(function () {
    'use strict';
    var app = angular.module('app.home', ['ui.router', 'app.account', 'app.stories'])
                     .config(['$stateProvider', function ($stateProvider) {

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
                    .controller("homeCtrl", ['$scope','StoryManager', function ($scope, mngr) {
                        $scope.mngr = mngr;
                    }]);

})();