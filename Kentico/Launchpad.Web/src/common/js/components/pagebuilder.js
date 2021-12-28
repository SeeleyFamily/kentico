/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import Carousel from './carousel';

class PageBuilder { 
    constructor() {

    }

    init() {
        this.startCarousel();
    }

    startCarousel() {
        const carousels = $('.carousel.carousel--page-builder');
        if (carousels.length > 0) {
            carousels.each((i, c) => {
                const car = new Carousel(c);
                car.init();
            });
        }
    }
}

export default PageBuilder;

$(() => {
	const pagebuilder = new PageBuilder();
	pagebuilder.init();
});