(function () {
    'use strict';
    var app = angular.module('app.stories', ['ui.router', 'app.account'])
                     .config(['$stateProvider', function ($stateProvider) {

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
                    .controller("storiesCtrl", function ($scope) {

                    });

})();