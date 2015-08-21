(function (module) {

    module.service('LocationManager', ['Location', '$http', 'serverName', '$q', function (Location, $http, serverName, $q) {

        this.getById = function (id) {
            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/Location/' + id, { cache: true }).then(
                function (response) {
                    deferred.resolve(new Location(response.data));
                }, function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        };
    }]);

})(angular.module('app.trips'));