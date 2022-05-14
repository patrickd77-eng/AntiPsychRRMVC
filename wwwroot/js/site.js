$(document).ready(function () {

    //Give footer current year
    updateFooterCopyrightDate();

    //Populate dropdownlist
    populateDrugSelectList();

});

function updateFooterCopyrightDate() {
    var today = new Date();
    $("#currentDate").html(today.getFullYear());
}

function clearFrequencyList() {
    $("#frequency").empty();
    populateFrequencyList();
}

function populateFrequencyList(route) {

    if (route == null) {
        route = $('#drugSelect :selected').attr("drugroute")
    }

    var url = "/Home/GetDoseFrequencies",
        drugRoute = route;

    $.ajax({
        url: url, async: true, data: {
            route: drugRoute
        }, success: function (result) {
            $.each(result, function (i, data) {
                //Increment to change from zero index to start at 1.
                i++

                //Contains three words
                if (data.split(/(?=[A-Z])/).length > 2) {

                    var seperateDoseFrequencyText =
                        data.split(/(?=[A-Z])/)[0] + " " +
                        data.split(/(?=[A-Z])/)[1] + " " +
                        data.split(/(?=[A-Z])/)[2]
                }
                //Contains two words
                else {
                    var seperateDoseFrequencyText =
                        data.split(/(?=[A-Z])/)[0] + " " +
                        data.split(/(?=[A-Z])/)[1]
                }

                var frequencyOption =
                    "<option name =\"" +
                    data + "\" value=\"" +
                    i + "\">" + seperateDoseFrequencyText +
                    "</option>"
                //Delete 'loading' message from select.
                $("#deleteTwo").remove();

                //Append each option to the select
                $("#frequency").append(frequencyOption);

            })
        }
    });
}

function populateDrugSelectList() {
    //Variables for building request.
    var url = "/Home/GetDrugList";

    //Make async request
    $.ajax({
        url: url, async: false, success: function (result) {
            $.each(result, function (i, data) {
                //Declare variables per array item in result
                var drugId = data.drugId,
                    drugName = data.drugName,
                    drugFrequency = data.drugFrequency.frequencyDetails,
                    drugRoute = data.drugRoute.routeName
                // drugMaximumDose = data.drugMaxDose.maximumDoseLimit;

                //Create HTML string for option syntax
                var drugOption =
                    "<option drugFrequency=\"" +
                    drugFrequency + "\"" +
                    "drugRoute=\"" + drugRoute
                    + "\"" + "name =\"" +
                    drugName + "\" value=\"" +
                    drugId + "\">" +
                    drugName + " " +
                    drugRoute + " " +
                    drugFrequency +
                    "</option>"
                //Delete 'loading' message from select.
                $("#delete").remove();

                //Append each option to the select
                $("#drugSelect").append(drugOption);
            });
            populateFrequencyList($('#drugSelect :selected').attr("drugroute"))
        }
    });
}

$('#scroll').click(function (e) {
    e.preventDefault();
    $("html, body").animate({ scrollTop: $("#drugTop").scrollTop() }, 1000);
});


$('#Reset').click(function (e) {
    location.reload();
});

$('#AddToDrugList').click(function (e) {
    //Prevent refresh
    e.preventDefault();

    //Variables for building request.
    var url = "/Home/ProcessSelectedDrug",
        drugId = $('#drugSelect').val(),
        drugFrequency = $('#frequency :selected').val(),
        drugRoute = $('#drugSelect :selected').attr("drugroute"),
        drugName = $('#drugSelect :selected').attr("name"),
        dose = $('#dose').val();

    if (dose == "") {
        alert("Enter a dose...")
    }
    else {

        $.ajax(
            {
                type: "POST", //HTTP POST Method
                async: true,
                url: url, // Controller/View 
                data: { //Passing data
                    id: drugId,
                    dose: dose,
                    frequencyModifier: drugFrequency,
                    route: drugRoute
                },
                success: function (result) {

                    $('#addedDrug').append(
                        "<tr><td><p>" + drugName + "</p></td>" +
                        "<td><p>" + drugFrequency + "</p></td>" +
                        "<td><p>" + drugRoute + "</p></td>" +
                        "<td><p>" + result['drugMaxDose'] + "</p></td>" +
                        "<td><p>" + result.dose + "</p></td>" +
                        "<td><p>" + result['doseUtilisation'] +
                        "%</p></td></tr>")

                    tallyPercentage(result);
                }
            });
    }
})

function tallyPercentage(result) {
    var getCurrentPercentage = $("#percentageMax").val();
    newPercentage = parseInt(getCurrentPercentage) + parseInt(result['doseUtilisation'])
    $("#percentageMax").val(newPercentage)
    checkMaxDoseUsage(newPercentage);
}

function checkMaxDoseUsage(newPercentage) {
    if (newPercentage >= 100) {
        //Display max dose usage warning 
        displayMaxDosePercentWarning();
    }
}

function displayMaxDosePercentWarning() {
    $("#warnings").show();
    $("#percentageWarning").text("High Dose Antipsychotics: the BNF maximum recommended dose percentage has been reached. However, you may continue to add drugs.");
    $("#overallMaxDose").addClass("alert-warning").removeClass("alert-info");
}