/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import slick from 'slick-carousel';
import * as CONST from '../utils/constants';
import Util from '../utils/ui';

class Carousel {
    constructor(target, settings) {
        this.carouselContainer = target;
        this.settings = settings || {
            mobileFirst: true,
            arrows: false,
            dots: true,
            centermode: false,
            infinite: true,
            slidesToShow: 2,
            slidesToScroll: 1,
            responsive: [
                {
                    breakpoint: 768,
                    settings: {
                        slidesToShow: 3,
                        slidesToScroll: 1,
                        infinite: true,
                        arrows: true,
                        dots: true
                    }
                }
            ]
        };
    }

    init() {
        $(this.carouselContainer).slick(this.settings);

		this.initCarousels();
		this.carouselWidget();        
        this.resetIframes();
    }

    initCarousels() {
        if ($('.carousel--related' && Util.getViewPort() !== CONST.ViewPort.Desktop).length) {
            $('.carousel--related').slick({
                arrows: false,
                dots: true,
                infinite: false
            });

            // Set height of slick slide and card body for equal height
            const stHeight = $('.slick-track').height();
            $('.slick-slide').css('height', `${stHeight}px`);
            $('.slick-slide .card-body-content-wrapper').css('height', `${stHeight - 130}px`);

            // Reset height of slick track
            const ssHeight = $('.slick-slide').height();
            $('.slick-track').css('height', `${ssHeight + 75}px`);
        }
    }

    carouselWidget() {
		$('.slider-for').slick({
			slidesToShow: 1,
			slidesToScroll: 1,
			arrows: false,
			fade: true,
			adaptiveHeight: true,
			asNavFor: '.slider-nav'
		});

		$('.slider-nav').slick({
			slidesToShow: 4,
			slidesToScroll: 1,
			asNavFor: '.slider-for',
			dots: false,
			infinite: true,
			arrows: true,
			focusOnSelect: true,
			mobileFirst: true,
		});
	}
    
    // this method is used to reset iframes that are playing on carousels on slide change
    resetIframes() {
        $(this.carouselContainer).on('afterChange', (event, slick, currentSlide, nextSlide) => {
            this.resetIframe();
        });
        $('.slider-for').on('afterChange', (event, slick, currentSlide, nextSlide) => {
            this.resetIframe();
        });
        $('.slider-nav').on('afterChange', (event, slick, currentSlide, nextSlide) => {
            this.resetIframe();
        });
    }

    resetIframe() {
        $('.carousel iframe').each((k, v) => {
            let elSrc = $(v).attr('src');
            $(v).attr('src', elSrc);
        });
    }    
}

export default Carousel;

$(() => {
	const carousel = new Carousel();
	carousel.init();
});