// simple-controls.js
(function () {

    angular.module("simple-controls", [])
        .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "/views/wait-cursor.html"
        };
    }

})();
