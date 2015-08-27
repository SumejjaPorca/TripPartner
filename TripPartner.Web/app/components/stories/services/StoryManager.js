(function (module) {

    module.service('StoryManager', ['Story', '$http', 'serverName', '$q', function (Story, $http, serverName, $q) {
      
    
        this.getAll = function (index) {
            var stories = [];

            var deferred = $q.defer();

            var url = '';

            if (index == '')
                url = 'http://' + serverName + '/api/Story/all';
            else 
                url = 'http://' + serverName + '/api/Story/' + index;

            $http.get(url).then(function (response) {
                var data = response.data;
                data.forEach(function (story) {
                    stories.push(new Story(story));
                });
                deferred.resolve(stories);
            },
                function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        }

        this.getByTripId = function (tripId) {
            var stories = [];

            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/Trip/' + tripId + '/Story').then(function (response) {
                var stories = response.data;
                stories.forEach(function (story) {
                    stories.push(new Story(story));
                });
                deferred.resolve(stories);
            },
                function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        }

        this.getByUserId = function (userId) {

            var stories = [];

            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/User/' + userId + '/Story').then(function (response) {
                var stories = response.data;
                stories.forEach(function (story) {
                    stories.push(new Story(story));
                });
                deferred.resolve(stories);
            },
                function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        };


        this.AddNew = function (newStory) {
            var deferred = $q.defer();

            $http({
                url: 'http://' + serverName + '/api/Story',
                method: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                data: newStory
            }).then(function (response) {
                deferred.resolve(response);
            },
            function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
      
    }]);

})(angular.module('app.stories'));