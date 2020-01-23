"use strict";
$(document).ready(function() {
var swiper = new Swiper('.swiper-container', {
        pagination: '.swiper-pagination',
        slidesPerView: 5,
        paginationClickable: true,
        spaceBetween: 30,
        loop: true,
        breakpoints:{
                480: {
                        slidesPerView: 1
                },
                767: {
                        slidesPerView: 2,
                        spaceBetween: 20
                },
                991: {
                        slidesPerView: 3,
                        spaceBetween: 30
                }
        }
    });
});