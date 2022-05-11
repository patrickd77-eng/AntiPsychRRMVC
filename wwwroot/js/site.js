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

    //Make async request
    $.ajax({
        url: url, async: true, success: function (result) {
            $.each(result, function (i, data) {
                //Declare variables per array item in result
                var drugId = data.drugId,
                    drugName = data.drugName,
                    drugFrequency = data.drugFrequency.frequencyDetails,
                    drugRoute = data.drugRoute.routeName,
                    drugMaximumDose = data.drugMaxDose.maximumDoseLimit;

                //Create HTML string for option syntax
                var drugOption =
                    "<option name=\"" +
                    drugName + "\" value=\"" + drugId + "\">" +
                    drugName + " " +
                    drugRoute + " " +
                    drugFrequency + " " +
                    drugMaximumDose +
                    "</option>"
                //Delete 'loading' message from select.
                $("#delete").remove();

                //Append each option to the select
                $("#drugSelect").append(drugOption);
            });
        }
    });
}


$('#Reset').click(function (e) {
    location.reload();
})

$('#AddToDrugList').click(function (e) {
    //Prevent refresh
    e.preventDefault();

    //Variables for building request.
    var url = "/Home/ProcessSelectedDrug",
        drugId = $('#drugSelect').val(),
        drugName = $('#drugSelect :selected').attr("name"),
        maxDose = $('#drugSelect :selected').attr("maxDose"),
        dose = $('#dose').val();

    $.ajax(
        {
            type: "POST", //HTTP POST Method
            async:true,
            url: url, // Controller/View 
            data: { //Passing data
                id: drugId,
                dose: dose
            },
            success: function (result) {
                console.log({ result });
            }
        });

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