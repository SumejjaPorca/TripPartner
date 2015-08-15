(function (module) {

    module.service('StoryManager', ['Story', '$http', 'serverName', '$q', function (Story, $http, serverName, $q) {
      
        var TopStories = undefined;//we will cache this because it doesn't change often
       
        this.getTopStories = function () {


            var deferred = $q.defer();

            if (TopStories == undefined)
                $http.get('http://' + serverName + '/api/Story?index=Rating').then(function (response) {
                    TopStories = response.data.stories;//TO DO: this
                    deferred.resolve(response);
                },
                function (response) {
                    deferred.reject(response);
                });
            else
                deferred.resolve(TopStories);

            return deferred.promise;
        };
      
    }]);

})(angular.module('app.stories'));