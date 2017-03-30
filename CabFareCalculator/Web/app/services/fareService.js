angular.module('fareService', [])

    // each function returns a promise object 
    .factory('Fares', function ($http) {
        return {
            create: function (fareData) {
                //$http({
                //    method: 'POST',
                //    data: fareData,
                //    url: '/api/fare/createFare',
                //    headers: {
                //        'Content-Type': "application/json",
                //        'Accept': "text/plain"
                //    },
                //    responseType: 'json'
                //});
                /*
                $http.post('/api/Fare/createFare', fareData).
                    success(function (data, status, headers, config) {
                        console.log("Success")
                    }).error(function (data, status, headers, config) {
                        console.log("Error");
                    });
                */
                return $http.post('/api/fare/createFare', fareData);
            }
        }
    });