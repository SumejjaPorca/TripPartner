(function (module) {

    module.factory('NewTrip', [ 'NewLocation', function (NewLocation) {
        function NewTrip(model) {
            this.DateStarted = model.DateStarted,
            this.DateEnded = model.DateEnded,
            this.Destination = new NewLocation(model.Destination),
            this.Origin = new NewLocation(model.Origin),
            this.CreatorId = model.CreatorId,
            this.CreatorUsername = model.CreatorUsername
        }
        return NewTrip;
    }]);

})(angular.module('app.trips'));