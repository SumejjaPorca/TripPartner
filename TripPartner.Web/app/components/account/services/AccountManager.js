﻿(function (module) {

    module.service('AccountManager', ['User', '$http', 'serverName', '$q', 'localStorageService', function (User, $http, serverName, $q, localStorageService) {
        var currentUser = undefined;

        var init = function () {
            currentUser = new User("", "", "");
        }

        init();

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
                currentUser.IsLoggedIn = false;
                localStorageService.set('authorizationData', { token: "", username: "" });
                deferred.reject(response);
            });

            return deferred.promise;
        };
        this.Logout = function () {
            localStorageService.set('authorizationData', { token: "", username: "" });
            currentUser.IsLoggedIn = false;
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
                currentUser.IsLoggedIn = false;
                localStorageService.set('authorizationData', { token: "", username: "" });
                deferred.reject(response);
            });

            return deferred.promise;
        };
        this.getCurrentUser = function () {
            return currentUser;
        };
        var setCurrentUser = function (loginModel) {
            currentUser.Username = loginModel.username;
            currentUser.Email = loginModel.email;
            currentUser.Password = loginModel.password;
            currentUser.IsLoggedIn = true;
        }
    }]);

})(angular.module('app.account'));