// start JQuery
$(document).ready(function () {


    // hide li elements
    $('#main_menu_item_page li').css('opacity', 0);

    // fade in in sequence
    $('#main_menu_item_page li').each(function (i) {
        $(this).delay((i++) * 200).fadeTo(1000, 1);
    })

});