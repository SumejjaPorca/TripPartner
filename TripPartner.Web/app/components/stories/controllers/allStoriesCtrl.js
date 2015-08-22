(function (storyModule) {

    storyModule.controller('allStoriesCtrl', ['$scope', 'StoryManager', '$state', function ($scope, mngr, $state) {


        var init = function () {

            $scope.message = "";

            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.

            mngr.getAll('').then(
                  function (response) {
                      $scope.Stories = response;
                      $scope.Title = "Stories";
                  }, function (response) {
                      $scope.message = "No stories were found."
                  });
        }


        init();



    }]);

}(angular.module("app.stories")));