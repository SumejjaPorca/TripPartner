(function (module) {

    module.service('StoryManager', ['Story', '$http', 'serverName', '$q', function (Story, $http, serverName, $q) {
      
    
        this.getTopStories = function () {

            var TopStories = [];

            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/Story/Rating').then(function (response) {
                    var stories = response.data;
                    stories.forEach(function (story) {
                        TopStories.push(new Story(story));
                    });
                    deferred.resolve(TopStories);
                },
                function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        };

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

        this.AddNew = function (newStory) {
            var deferred = $q.defer();

            $http({
                url: 'http://' + serverName + '/api/Story',
                method: "POST",
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