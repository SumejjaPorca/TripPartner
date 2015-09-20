(function (module) {

    module.service('GeolocationService', ['Location', '$q', function (Location, $q) {

        var Geocoder = geocoder = new google.maps.Geocoder();

        this.instantiateMap = function (elementId, mapProp) {
            var prop = {
                center: new google.maps.LatLng(mapProp.center.Lat, mapProp.center.Lng),
                zoom: mapProp.zoom,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var element = document.getElementById(elementId);
           return new google.maps.Map(element, prop);
        }

        this.Marker = function (map, pos) {
            var map = map;
            var pos = pos;
            var marker = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(pos.lat, pos.lng)
            });
            this.hide = function () {
                marker.setMap(null);
            }
            this.show = function () {
                marker.setMap(map);
            };
            this.changeMap = function (newMap) {
                map = newMap;
                marker.setMap(map);
            }
            this.changePosition = function(newPos){
                pos = newPos;
                marker.setPosition(new google.maps.LatLng(newPos.H, newPos.L));
                marker.setMap(map);
            }
            this.getPosition = function () {
                return pos;
            }
        }

        this.getLatLng = function (address) {

            var deferred = $q.defer();

            Geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    deferred.resolve(results[0].geometry.location);
                }
                else {
                    deferred.reject(status);
                }
            });
            return deferred.promise;
        }

    }])

})(angular.module('app.trips'));