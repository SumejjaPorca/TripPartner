(function (module) {

    module.service('AccountManager', ['User', '$http', 'serverName', '$q', 'localStorageService', function (User, $http, serverName, $q, localStorageService) {
      
        var currentUser = new User({ username: "", email: "", password: "", id: "" });
  
        this.getUserInfo = function (userId) {
            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/Account/UserInfo/' + userId).then(
                function (response) {
                    deferred.resolve(response.data);
                }, function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        };

        this.Login = function (loginModel) {

            var deferred = $q.defer();

            $http({
                url: 'http://' + serverName + '/Token',
                method: "POST",
                data: 'grant_type=password&username=' + loginModel.username + '&password=' + loginModel.password
            }).then(function (response) {
                localStorageService.set('authorizationData', { token: response.data.access_token, userName: response.data.userName, email: response.data.email, id: response.data.id});
                setCurrentUser(response.data);
                deferred.resolve(response);
            }, 
            function (response) {
                localStorageService.set('authorizationData', { token: "", userName: "" });
                currentUser.IsLoggedIn = false;
                deferred.reject(response);
            });

            return deferred.promise;
        };
        this.Logout = function () {
            localStorageService.set('authorizationData', { token: "", userName: "" });
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
                localStorageService.set('authorizationData', { token: "", userName: "" });
                currentUser.IsLoggedIn = false;
                deferred.reject(response);
            });

            return deferred.promise;
        };
        this.getCurrentUser = function () {
            var data = localStorageService.get('authorizationData');
            if (currentUser.Id == "" && data.token != "")
            {
                setCurrentUser(data);
            }
            return currentUser;
        };
        var setCurrentUser = function (loginModel) {
            currentUser.Username = loginModel.userName;
            currentUser.Email = loginModel.email;
            currentUser.Id = loginModel.id;
            currentUser.IsLoggedIn = true;
        }
    }]);

})(angular.module('app.account'));