/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import * as CONST from '../utils/constants';
import Util from '../utils/ui';
import 'bootstrap';
import 'bootstrap-select';

class DropDown {
    constructor() {
    }

    init() {
        this.bindEvents();
    }

	bindEvents() {
        if ($('.js-select').length) {
            $('.js-select').each((index, element) => {
                let settings = {
                    mobile: true,
                    liveSearchPlaceholder: 'Type to search',
                    dropupAuto: false,
                    size: '7'
                };

                if ($(element).data('custom-mobile')) {
                    settings.mobile = false;
                }

                $(element).selectpicker(settings);
            });
            
            // Dropdown to anchor
            $('.js-select-anchor').on('changed.bs.select', (e, clickedIndex, isSelected, previousValue) => {
                const selection = e.target.selectedOptions[0].value;

                if (selection.length > 0) {
                    $('html, body').animate({
                        scrollTop: ($(`#${selection}`).offset().top - 100)
                    }, 2500);

                    if (!$(`#${selection}`).hasClass('active')) {
                        $(`#${selection}`).click();
                    }
                }
            });

            $('.js-select-anchor').on('shown.bs.select', (e) => {
                const previousQuery = $('.filter-option-inner-inner').text();
                if (previousQuery !== 'Type to search') {
                    $('.bs-searchbox input').val(previousQuery);
                }
                $('.bs-searchbox input').focus();

                document.addEventListener('keydown', (event) => {
                    const parent = $('.dropdown-menu div.inner');
                    const active = $('ul.dropdown-menu .active');

                    if (event.key === 'ArrowDown' || event.key === 'ArrowUp') {
                        parent.scrollTop(parent.scrollTop() + (active.position().top - parent.position().top) - (parent.height() / 2) + (active.height() / 2));
                    }
                });
            });
            //---/ End dropdown to anchor
        }
    }
}

export default DropDown;

$(() => {
	const dropdown = new DropDown();
	dropdown.init();
});