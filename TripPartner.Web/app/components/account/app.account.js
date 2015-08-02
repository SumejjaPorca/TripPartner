(function () {
    'use strict';
    angular.module('app.account', [])
                     .config(['$stateProvider', function ($stateProvider) {

                         $stateProvider
                    .state('login', {
                        url: '/login',
                        controller: 'loginCtrl',
                        templateUrl: '/app/components/account/partials/login.html'
                    })
                    .state('register', {
                        url: '/register',
                        controller: 'registerCtrl',
                        templateUrl: '/app/components/account/partials/register.html'
                    })

                  ;
                     }
                     ])
                     .run(function () {
                         alert('I am in app.account.js!');
                     });

})();