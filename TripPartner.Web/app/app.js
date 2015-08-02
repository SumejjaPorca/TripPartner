(function () {
    'use strict';
    var app = angular.module('app', ['ui.router.state', 'ui.router', 'app.account', 'app.home', 'app.stories'])
                     .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {


                     $urlRouterProvider.otherwise('/home');
                        
                     }
                     ])
                     .controller('indexCtrl', function ($scope) {
                         $scope.isLoggedIn = false;
                         $scope.currentUser = undefined;
                     })
                     .run(function () {
                         alert('I am in app.js!');
                     });

})();