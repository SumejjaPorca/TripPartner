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
            this.TripId = model.TripId,
            this.Title = model.Title,
            this.Rating = model.Rating,
            this.Rates = model.Rates
        }
        return Story;
    });

})(angular.module('app.stories'));