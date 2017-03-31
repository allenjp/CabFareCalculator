angular.module('fareController', [])
     // inject the Fare service factory into our controller
    .controller('mainController', function ($scope, $http, Fares) {

        $scope.formData = {};
        $scope.fares = [];

        // when submitting the fare form, send the data to the C# API
        $scope.createFare = function () {

            var errors = false;
            var errorMessage = "";

            // validate the formData to make sure input is in the correct format

            // make sure all fields have been filled out
            $('input').each(function (i) {
                if ($(this).val() == "") {
                    errors = true;
                    errorMessage = "Please fill out all fields";
                }
            });

            $('select').each(function (i) {
                if ($(this).val() == "") {
                    errors = true;
                    errorMessage = "Please fill out all fields";
                }
            });

            if (!errors) {
                // make sure Minutes and Miles inputs are numeric
                if (isNaN($scope.formData.minsAbove6) || isNaN($scope.formData.milesBelow6)) {
                    errors = true;
                    errorMessage = "Minutes above 6 MPH and Miles below 6 MPH must both be numeric";
                }

                else {
                    // if we have no validation errors proceed with passing the data to the api
                    // call the create function from our service (returns a promise object)
                    Fares.create($scope.formData)

                        // if successful creation, call our get function to get all the new fares
                        .success(function (data) {

                            // make sure we didn't get an error
                            if (data.status == 0) {
                                alert(data.message);
                            }
                            else {
                                $scope.formData = {}; // clear the form so our user is ready to enter another
                                $scope.fares.push(data); // assign our new list of fares
                                console.log($scope.fares);
                            }             
                        })
                        .error(function (data) {
                            console.log('Error: ' + data);
                        });
                }
            }
            else {
                alert(errorMessage);
            }
        };
});