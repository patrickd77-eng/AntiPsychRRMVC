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

function populateFrequencyList() {

    var url = "/Home/GetDrugTypeFrequencies",
        drugId = $('#drugSelect').val(),
        drugRoute = $('#drugSelect :selected').attr("drugroute"),
        dose = $('#dose').val();

    $.ajax({
        url: url, async: true, data: { //Passing data
            dose: dose,
            route: drugRoute
        }, success: function (result) {


        }
    });

}

function populateDrugSelectList() {
    //Variables for building request.
    var url = "/Home/GetDrugList";

    //Make async request
    $.ajax({
        url: url, async: true, success: function (result) {
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
        drugFrequency = $('#drugSelect :selected').attr("drugfrequency"),
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
                    dose: dose
                },
                success: function (result) {

                    $('#addedDrug').append(
                        "<tr><td><p>" + drugName + "</p></td>" +
                        "<td><p>" + drugFrequency + "</p></td>" +
                        "<td><p>" + drugRoute + "</p></td>" +
                        "<td><p>" + result['drugMaxDose'] + "</p></td>" +
                        "<td><p>" + dose + "</p></td>" +
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