(function () {
    'use strict';
    var app = angular.module('app.trips', ['ui.router'])
                     .config(['$stateProvider', function ($stateProvider) {

                         $stateProvider
                    .state('trips', {
                        url: '/trips',
                        controller: 'tripsCtrl',
                        // templateUrl: '/app/components/stories/partials/trips.html'
                    })

                         ;
                     }
                     ])
                     .run(function () {
                     })
                    .controller("tripsCtrl", function ($scope) {

                    });

})();