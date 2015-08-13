(function (module) {

    module.factory('Trip', function () {
        function Trip(model) {
            this.Id = model.Id,
            this.DateStarted = model.DateStarted,
            this.DateEnded = model.DateEnded,
            this.Destination = model.Destination,
            this.Origin = model.Origin,
            this.CreatorId = model.CreatorId,
            this.CreatorUsername = model.CreatorUsername
        }
        return Trip;
    });

})(angular.module('app.trips'));