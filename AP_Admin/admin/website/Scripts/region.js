/// <reference path="jquery-1.4.1.min.js" />
/// <reference path="jquery-1.4.1-vsdoc.js" />
function showRegion(regionClickImageName, regionName) {
    var a1 = $("#"+regionClickImageName);
    var a2 = $("#"+regionName);
    if (a1 != null && a2 != null) {
        if (a1.attr("src") == "../img/tit_r.png") {
            a2.slideUp("slow");
            a1.attr("src", "../img/tit_dr.png");
        }
        else {
            a2.slideDown("slow");
            a1.attr("src", "../img/tit_r.png");
        }

    }
}