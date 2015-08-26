(function (storyModule) {

    storyModule.controller('storiesByTripCtrl', ['$scope', 'StoryManager', '$stateParams', function ($scope, mngr, $stateParams) {


        var init = function () {

            $scope.Serial = $stateParams.serial;
            if ($scope.Serial == undefined)
                $scope.Serial = -1;
            $scope.message = "";

            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.

            mngr.getByTripId($stateParams.tripId).then(
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