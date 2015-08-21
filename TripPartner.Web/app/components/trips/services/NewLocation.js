(function (module) {

    module.factory('NewLocation', function () {
        function NewLocation(model) {
            this.Lat = model.Lat,
            this.Long = model.Long,
            this.Address = model.Address
        }
        return NewLocation;
    });

})(angular.module('app.trips'));