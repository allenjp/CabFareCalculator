angular.module('fareApp', ['fareController', 'fareService']);

$(document).ready(function () {

    // datepicker stuff
    $('#inputDate').datepicker();

    // dynamically populate selects in form
    hrSelect = document.getElementById('inputHour');
    minSelect = document.getElementById('inputMin');
    ampmSelect = document.getElementById('inputAmPm')

    hrSelect.options[0] = new Option("", "");
    for (var i = 1; i <= 12; i++) {
        hrSelect.options[i] = new Option(i, i);
    }

    minSelect.options[0] = new Option("", "");
    for (var j = 0; j <= 59; j++) {
        if (j < 10) {
            var format_min = "0" + j;
            minSelect.options[j + 1] = new Option(format_min, format_min);
        }
        else {
            minSelect.options[j] = new Option(j, j);
        }
    }
    ampmSelect.options[0] = new Option("", "");
    ampmSelect.options[1] = new Option("AM", "AM");
    ampmSelect.options[2] = new Option("PM", "PM");
});