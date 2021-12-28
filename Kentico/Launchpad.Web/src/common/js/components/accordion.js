/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import * as CONST from '../utils/constants';

class Accordion {
    constructor() {
    }

    init() {
        this.bindEvents();
    }

    bindEvents() {
        // Title click
        $(document).on('click', '.js-accordion-title', e => {
            const accordion = $(e.currentTarget);

            // Toggle active class
            $(accordion).toggleClass('active');

            // Toggle content visibility
            $(accordion).next('.js-accordion-content')
                .stop()
                .toggleClass('active');
        });

        $(document).on('keypress', '.js-accordion-title', e => {
            if (e.which === 13) {
                const accordion = $(e.currentTarget);

                // Toggle active class
                $(accordion).toggleClass('active');
    
                // Toggle content visibility
                $(accordion).next('.js-accordion-content')
                    .stop()
                    .toggleClass('active');
            }
        });
    }
}

export default Accordion;

$(() => {
	const accordion = new Accordion();
	accordion.init();
});