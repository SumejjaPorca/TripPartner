(function (module) {
    module.factory('authInterceptor', [ 'localStorageService', function (localStorageService) {

        return{ 

            request: function (config) {

                config.headers = config.headers || {};

                var authData = localStorageService.get('authorizationData');
                if (authData) {
                    config.headers.Authorization = 'Bearer ' + authData.token;
                }

                return config;
            }
        };

    }]);
})(angular.module('app.account'));