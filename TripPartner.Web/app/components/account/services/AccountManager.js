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
                loginModel.email = response.data.email;
                setCurrentUser(loginModel);
                localStorageService.set('authorizationData', { token: response.data.access_token, username: loginModel.username });
                deferred.resolve(response);
            }, 
            function (response) {
                currentUser = undefined;
                localStorageService.set('authorizationData', { token: "", username: "" });
                deferred.reject(response);
            });

            return deferred.promise;
        };
        this.Logout = function () {
            localStorageService.set('authorizationData', { token: "", username: "" });
            currentUser = undefined;
        };
        this.Register = function (registerModel) { 
            var deferred = $q.defer();

            $http({
                url: 'http://' + serverName + '/api/Account/Register',
                method: "POST",
                data: 'email=' + registerModel.email + '&password=' + registerModel.password + '&confirmPassword=' + registerModel.confirmPassword
            }).then(function (response) {
                 deferred.resolve(response);
            },
            function (response) {
                currentUser = undefined;
                localStorageService.set('authorizationData', { token: "", username: "" });
                deferred.reject(response);
            });

            return deferred.promise;
        };
        this.getCurrentUser = function () {
            return currentUser;
        };
        var setCurrentUser = function (loginModel) {
            currentUser = new User(loginModel);
        }
    }]);

})(angular.module('app.account'));