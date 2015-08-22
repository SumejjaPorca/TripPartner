(function (storyModule) {

    storyModule.controller('listCtrl', ['$scope', 'StoryManager', '$stateParams', 'index', function ($scope, mngr, $stateParams, index) {


        var init = function () {

            $scope.message = "";

            // A definitive place to put everything that needs to run when the controller starts. Avoid
            //  writing any code outside of this function that executes immediately.

            if ($stateParams.stories == null) {
                mngr.getAll(index).then(function (response) {
                    $scope.Stories = response;
                    if ($stateParams.serial == null)
                        serial = $scope.Stories / 2;
                    else {

                        if ($stateParams.serial >= $scope.Stories.length || $stateParams.serial < 0)
                            $scope.message = "Serial number is invalid.";
                        else
                            $scope.serial = $stateParams.serial;
                    }
                }, function (response) {
                    $scope.message = response.data;
                }
                    );
            }
            else {
                $scope.Stories = $stateParams.stories;
                if ($stateParams.serial == null)
                    serial = $scope.Stories / 2;
                else {

                    if ($stateParams.serial >= $scope.Stories.length || $stateParams.serial < 0)
                        $scope.message = "Serial number is invalid.";
                    else
                        $scope.serial = $stateParams.serial;
                }
            }
        }


        init();



    }]);

}(angular.module("app.stories")));