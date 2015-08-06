(function () {
    'use strict';
    var app = angular.module('app', ['ui.router.state', 'ui.router', 'app.account', 'app.home', 'app.stories'])
                     .config(['$stateProvider', '$urlRouterProvider', '$httpProvider', function ($stateProvider, $urlRouterProvider, $httpProvider) {


                         $urlRouterProvider.otherwise('/home');

                         //$httpProvider.defaults.withCredentials = true;
                        
                     }
                     ])
                    .constant('serverName', 'localhost:9010')

                     .controller('indexCtrl', ['$scope', 'AccountManager', function ($scope, mngr) {
                         $scope.mngr = mngr;
                     }])
                     .run(function () {
                         alert('I am in app.js!');
                     });

})();