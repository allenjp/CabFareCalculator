angular.module('fareController', [])
     // inject the Fare service factory into our controller
    .controller('mainController', function ($scope, $http, Fares) {

        $scope.formData = {};

        // when submitting the fare form, send the data to the C# API
        $scope.createFare = function () {

            // validate the formData to make sure input is in the correct format
            // if form is empty, nothing will happen
            if (!$.isEmptyObject($scope.formData)) {

                /*
                *
                VALIDATION WILL OCCUR HERE
                *
                */

                // call the create function from our service (returns a promise object)
                Fares.create($scope.formData)

                    // if successful creation, call our get function to get all the new fares
                    .success(function (data) {
                        $scope.formData = {}; // clear the form so our user is ready to enter another
                        $scope.fares = data; // assign our new list of fares
                    })
                    .error(function(data) {
                        console.log('Error: ' + data);
                    });
            }
        };
});