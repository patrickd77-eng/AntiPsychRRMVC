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

function populateDrugSelectList() {
    //Variables for building request.
    var url = "/Home/GetDrugList";

        //Make request
        $.get(url, function (result) {

            console.log({ result })
            console.log(result.drugName
                + " " + result.drugFrequency.frequencyDetails
                + " " + result.drugMaxDose.MaximumDoseLimit
                + " " + result.drugRoute.routeName)
            // TODO: Add to select list
            result.each(function () {



                $("#drug").append("option")
                    //< option value="" name="" selected > Choose a drug...</option >

            })

        });
}


$('#Reset').click(function (e) {
    location.reload();
})

$('#AddToDrugList').click(function (e) {
    //Prevent refresh
    e.preventDefault();

    //    //Variables for building request.
    //    var url = "/Home/ProcessSelectedDrug",
    //        drugId = $('#drug').val(),
    //        drugName = $('#drug :selected').attr("drugName"),
    //        maxDose = $('#drug :selected').attr("maxDose"),
    //        dose = $('#dose').val();

    //    //Make request
    //    $.get(url, { dose: dose, drugId: drugId, drugName: drugName, maxDose: maxDose }, function (result) {
    //        //Append new data if no errors.
    //        if (!result.errors.trim()) {

    //            $("#resultsArea").show();
    //            createResultsHtml(result);
    //            tallyPercentage(result);
    //        }
    //        else {
    //            alert("Did you forget to enter a dose? Please try again.");
    //        }
    //    });
})

//function createResultsHtml(result) {
//    $("#resultData").append("<tr class=\"card alert-info\"><td>"
//        + "<p><b>Drug</b>: " + result.drugName + "</p>"
//        + "<p><b>Dose Entered:</b> " + result.dose + "</p>"
//        + "<p><b>Max Dose:</b> " + result.maxDose + "</p>"
//        + "<p><b>Max dose utilisation (this drug):</b> " + result.maxDoseUtilisation + "%" + "</p>"
//        + "<br></td></tr>");
//}

//function tallyPercentage(result) {
//    var getCurrentPercentage = $("#percentage").val();
//    newPercentage = parseInt(getCurrentPercentage) + parseInt(result.maxDoseUtilisation)
//    $("#percentage").val(newPercentage)
//    checkMaxDoseUsage(newPercentage);
//}

//function checkMaxDoseUsage(newPercentage) {
//    if (newPercentage >= 100) {
//        //Display max dose usage warning 
//        displayMaxDosePercentWarning();
//    }
//}

//function displayMaxDosePercentWarning() {
//    $("#warnings").show();
//    $("#percentageWarning").text("The maximum recommended dose percentage has been reached.");
//    $("#utilisationContainer").addClass("alert-warning").removeClass("alert-info");


//}