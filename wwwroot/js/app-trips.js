// app-trips.js
(function () {

    angular.module("app-trips", ["simple-controls", "ngRoute"])
        .config(function ($routeProvider) {
            
            $routeProvider.when("/", {
                controller: "trips-controller",
                controllerAs: "vm",
                templateUrl: "/views/trips-view.html"
            });

            $routeProvider.when("/editor/:tripName", {
                controller: "trip-editor-controller",
                controllerAs: "vm",
                templateUrl: "/views/trip-editor-view.html"
            });

            $routeProvider.otherwise({ redirectTo: "/" });

        });

})();
