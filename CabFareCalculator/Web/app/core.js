angular.module('fareApp', ['fareController', 'fareService']);

// datepicker stuff
$(document).ready(function (){
    $('#inputDate').datepicker();
});