angular.module('fareService', [])

    // each function returns a promise object 
    .factory('Fares', function ($http) {
        return {
            create: function (fareData) {
                return $http.post('/api/fare/createFare', fareData);
            }
        }
    });