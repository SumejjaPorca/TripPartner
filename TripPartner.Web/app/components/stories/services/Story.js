﻿(function (module) {

    module.factory('Story', function () {
        function Story(model) {
            this.Id = model.Id,
            this.DateMade = model.DateMade,
            this.LastEdit = model.LastEdit,
            this.Date = model.Date,
            this.Text = model.Text,
            this.CreatorId = model.CreatorId,
            this.CreatorUsername = model.CreatorUsername,
            Trip = model.Trip
        }
        return Story;
    });

})(angular.module('app.stories'));