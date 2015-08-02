(function () {
    'use strict';
    var app = angular.module('app.home', ['ui.router', 'app.account'])
                     .config(['$stateProvider', function ($stateProvider) {

                         $stateProvider
                    .state('home', {
                        url: '/home',
                        controller: 'homeCtrl',
                        templateUrl: '/app/components/home/partials/home.html'
                    })

                   ;
                     }
                     ])
                     .run(function () {
                         alert('I am in app.home.js!');
                     })
                    .controller("homeCtrl", function ($scope) {

                    });

})();