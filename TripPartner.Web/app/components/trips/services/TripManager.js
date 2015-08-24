(function (module) {

    module.service('TripManager', ['Trip', '$http', 'serverName', '$q', function (Trip, $http, serverName, $q) {

        this.getById = function (id) {
            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/Trip/' + id, { cache: true }).then(
                function (response) {
                    deferred.resolve(new Trip(response.data));
                }, function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        };

        this.getByUserId = function (userId) {
            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/User/' + userId + '/Trip').then(
                function (response) {
                    var trips = [];
                    var data = response.data;
                    data.forEach(function (trip) {
                        trips.push(new Trip(trip));
                    });
                    deferred.resolve(trips);
                }, function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        };

        this.getByLocId = function (locId) {
            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/Location/' + locId + '/Trip').then(
                function (response) {
                    var trips = [];
                    var data = response.data;
                    data.forEach(function (trip) {
                        trips.push(new Trip(trip));
                    });
                    deferred.resolve(trips);
                }, function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        };

        this.getAll = function () {
            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/Trip').then(
                function (response) {
                    var trips = [];
                    var data = response.data;
                    data.forEach(function (trip) {
                        trips.push(new Trip(trip));
                    });
                    deferred.resolve(trips);
                }, function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        };

        this.AddNew = function (newTrip) {
            var deferred = $q.defer();

            $http({
                url: 'http://' + serverName + '/api/Trip',
                method: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                data: newTrip
            }).then(function (response) {
                deferred.resolve(response);
            },
            function (response) {
               deferred.reject(response);
            });

            return deferred.promise;
        };

    }]);

})(angular.module('app.trips'));