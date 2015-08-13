(function (module) {

    module.service('StoryManager', ['Story', '$http', 'serverName', '$q', function (Story, $http, serverName, $q) {
      
       
        this.getTopStories = function (loginModel) {

            var deferred = $q.defer();

            $http.get('http://' + serverName + '/api/Story?index=Rating').then(function (response) {
                var obj = new Story(response.story);//TO DO: this
                deferred.resolve(obj);
            },
            function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
      
    }]);

})(angular.module('app.stories'));