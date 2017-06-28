function mobile_menu() {
    $(window).width();

    if ($(window).width() <= 600) {

        // hide menu items before click
        $("#menu_items").css('display', 'none');

        // toggle menu. Unbind the click during the resize
        $("#toggle_menu_mobile").unbind("click").click(function () {

            // animation for the menu
            $("#menu_items").animate({
                height: "toggle"
            }, 300);

        })
    } else {

        // display nav var if it was hidden when resizing to a bigger width
        $("#menu_items").css('display', 'block');

        // disables the click effect
        $("#toggle_menu_mobile").unbind("click")

    }
}



$(document).ready(function () {
    // calling the function on load
    mobile_menu();

    $(window).resize(function () {
        // calling the function on resize
        mobile_menu();
    })


})// end of document. ready