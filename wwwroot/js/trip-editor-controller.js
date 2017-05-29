// trips-controller.js
(function () {

    angular.module("app-trips")
        .controller("trip-editor-controller", tripEditorController);

    function tripEditorController($http, $routeParams) {

        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        $http.get("/api/trips/" + vm.tripName + "/stops")
            .then(function (response) {
                angular.copy(response.data, vm.stops);
                _showMap(vm.stops);
                vm.errorMessage = "";
            }, function (error) {
                vm.errorMessage = "Failed to load stops";
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addStop = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips/" + vm.tripName + "/stops", vm.newStop)
                .then(function (response) {
                    vm.stops.push(response.data);
                    _showMap(vm.stops);
                    vm.newStop = {};
                }, function (error) {
                    vm.errorMessage = "Failed to save new stop";
                })
                .finally(function () {
                    vm.isBusy = false;
                });

        };
    }

    function _showMap(stops) {
        if (stops && stop.length > 0) {

            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                }
            });

            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3
            });
        }
    }

})();
