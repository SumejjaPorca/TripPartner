(function (module) {

    module.service('AccountManager', ['User', '$http', 'serverName', '$q', 'localStorageService', function (User, $http, serverName, $q, localStorageService) {
        var currentUser = undefined;

        this.Login = function (loginModel) {

            var deferred = $q.defer();

            $http({
                url: 'http://' + serverName + '/Token',
                method: "POST",
                data: 'grant_type=password&username=' + loginModel.username + '&password=' + loginModel.password
            }).then(function (response) {
                setCurrentUser(loginModel);
                localStorageService.set('authorizationData', { token: response.access_token, username: loginModel.username });
                deferred.resolve(response);
            }, 
            function (err) {
                currentUser = undefined;
                localStorageService.set('authorizationData', { token: "", username: "" });
                deferred.reject(err);
            });

            return deferred.promise;
        };
        this.Logout = function () {
            localStorageService.set('authorizationData', { token: "", username: "" });
            currentUser = undefined;
        };
        this.Register = function (registerModel) { //TO DO: this

        };
        this.getCurrentUser = function () {
            return currentUser;
        };
        var setCurrentUser = function (loginModel) {
            currentUser = new User(loginModel);
        }
    }]);

})(angular.module('app.account'));