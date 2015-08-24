(function (storyModule) {

    storyModule.controller('addStoryCtrl', ['$scope', 'StoryManager', '$stateParams', 'AccountManager', 'TripManager', function ($scope, mngr, $stateParams, accMngr, tripMngr) {


        var init = function () {

            $scope.ok = true;
            $scope.message = '';
            var user = accMngr.getCurrentUser();
            if (accMngr.getCurrentUser.IsLoggedIn == false) {
                $scope.message = 'You must be logged in first to add a new story.';
                $scope.ok = false;
            }
            var trip = undefined;

            tripMngr.getById($stateParams.tripId).then(function (response) {
                trip = response;
            },
                function (response) {
                    $scope.message = "The trip id: " + $stateParams.tripId + " is not valid.";
                    $scope.ok = false;
                });

            var today = new Date();

            $scope.newStory = {
                DateMade : today,
                LastEdit : today,
                Date : today,
                Text : "",
                CreatorId : user.Id,
                CreatorUsername : user.Username,
                TripId : $stateParams.tripId,
                Title : "",
                Rating : 0,
                Rates : 0
            };



            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.

        }

        init();

 
        $scope.submit = function () {
            mngr.AddNew($scope.newStory).then(
                function (response) {
                    $scope.message = "Story is successfully added to your collection of stories."
                }, function (response) {
                    $scope.ok = false;
                    $scope.message = response.data;
                });

        }

    }]);

}(angular.module("app.stories")));