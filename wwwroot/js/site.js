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
                    "<option drugFrequency=\"" +
                    drugFrequency + "\"" +
                    "drugRoute=\"" + drugRoute
                    + "\"" + "name =\"" +
                    drugName + "\" value=\"" +
                    drugId + "\">" +
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
        drugFrequency = $('#drugSelect :selected').attr("drugfrequency"),
        drugRoute = $('#drugSelect :selected').attr("drugroute"),
        drugName = $('#drugSelect :selected').attr("name"),
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

                console.log({result})

                $('#addedDrug').append("<td><p>" + drugName + "</p></td>")
                $('#addedDrug').append("<td><p>" + drugFrequency + "</p></td>")
                $('#addedDrug').append("<td><p>" + drugRoute + "</p></td>")
                $('#addedDrug').append("<td><p>" + result['drugMaxDose'] + "</p></td>")
                $('#addedDrug').append("<td><p>" + dose + "</p></td>")
                $('#addedDrug').append("<td><p>" + result['doseUtilisation'] + "%</p></td>")

            }
        });

})

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