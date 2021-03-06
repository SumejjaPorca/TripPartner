﻿(function (module) {

    module.factory('Trip', ['Location', function (Location) {
        function Trip(model) {
            this.Id = model.Id,
            this.DateStarted = model.DateStarted,
            this.DateEnded = model.DateEnded,
            this.Destination = new Location(model.Destination),
            this.Origin = new Location(model.Origin),
            this.CreatorId = model.CreatorId,
            this.CreatorUsername = model.CreatorUsername
        }
        return Trip;
    }]);

})(angular.module('app.trips'));