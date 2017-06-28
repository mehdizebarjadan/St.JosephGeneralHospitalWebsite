$(document).ready(function () {

    // incrementer for the array index
    var incrementedIndex = 0;

    ////dynamically get the lenght of the featured news coming from DB
    var endIndex = $('#home_newsfeed div.featured_news_box').length;


    // make sure there are more than one news entry is coming from DB, before using looping animation

    if (endIndex !== 1) {

        function newsFeedLoop() {

            // Fade in the first news in two seconds. Then holds it for 4 seconds
            // then fades out in 2 seconds. Finally the function to call itself
            $('#home_newsfeed div.featured_news_box:eq(' + incrementedIndex + ')').fadeIn(1000).delay(6000).fadeOut(1000, newsFeedLoop);

            // add one to the incrementer
            incrementedIndex++

            // if the incrementer reaches the lenght of the array, get restarted
            if (incrementedIndex === endIndex) {

                incrementedIndex = 0;
            } // end reset incremented index if statement

        } // end newsFeedLoop() function

        // call the function
        newsFeedLoop();

    } else {
        // Else meaning there is only 1 entry coming from DB. So it only display it, no animation required 

        $('.featured_news_box').css('display', 'block');
    }

   

}); // end document.ready