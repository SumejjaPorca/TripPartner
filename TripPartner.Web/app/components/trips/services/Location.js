﻿(function (module) {

    module.factory('Location', function () {
        function Location(model) {
            this.Id = model.Id,
            this.Lat = model.Lat,
            this.Long = model.Long,
            this.Address = model.Address
        }
        return Location;
    });

})(angular.module('app.trips'));