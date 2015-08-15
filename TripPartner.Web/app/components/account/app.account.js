(function () {
    'use strict';
    angular.module('app.account', ['LocalStorageModule'])
                     .config(['$stateProvider', '$httpProvider', function ($stateProvider, $httpProvider) {

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
                         .state('profile', { //TO DO: add some parameters
                             url: '/profile',
                             controller: 'profileCtrl',
                             templateUrl: '/app/components/account/partials/profile.html'
                         });
                         $httpProvider.interceptors.push('authInterceptor');
                         $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';


                     }
                     ])
                     .run(function () {
                     });

})();