jQuery( document ).ready(function( $ ) {
"use strict"
/*-----------------------------------------------------------------------------------*/
/*    PORTFOLIO FILTER
/*-----------------------------------------------------------------------------------*/
    $('#Container').mixItUp();

    var pathname = location.pathname
    $(".nav-link").each(function () {
        if ($(this).attr('href') == pathname) {
            $(this).addClass('active')
        }
    })
});





