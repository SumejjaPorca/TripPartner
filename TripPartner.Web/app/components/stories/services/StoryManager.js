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
      
    }]);

})(angular.module('app.stories'));