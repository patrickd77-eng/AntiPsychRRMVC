$(document).ready(function () {

    //Give footer current year
    updateFooterCopyrightDate();

});

function updateFooterCopyrightDate() {
    var today = new Date();
    $("#currentDate").html(today.getFullYear());
}
