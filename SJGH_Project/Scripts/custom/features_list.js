// start JQuery
$(document).ready(function () {


    // hide li elements
    $('.cmsfeatures li').css('opacity', 0);

    // fade in in sequence
    $('.cmsfeatures li').each(function (i) {
        $(this).delay((i++) * 200).fadeTo(1000, 1);
    })

});