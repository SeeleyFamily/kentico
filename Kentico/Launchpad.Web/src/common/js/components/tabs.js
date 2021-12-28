/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import * as CONST from '../utils/constants';
import Util from '../utils/ui';
import 'bootstrap';

class Tabs {
    constructor() {
    }

    init() {
        this.bindEvents();
    }

    bindEvents() {
        $('button[data-bs-toggle="tab"]').on('shown.bs.tab', (event) => {
            let parent = $(event.target).closest('.tab-container');
            let elementTarget = $(event.target).data('bs-target');

            // reset accordion buttons
            $(parent).find('.tab-accordion-button').addClass('collapsed');
            $(parent).find(`.tab-accordion-button[data-bs-target="${elementTarget}"]`).removeClass('collapsed');
            
            // reset body class
            $(parent).find('.tab-pane').removeClass('show');
            $(parent).find(elementTarget).addClass('show');
        });

        $(document).on('shown.bs.collapse', '.tab-container', (event) => {
            let parent = $(event.target).closest('.tab-container');

            // reset tab links
            $(parent).find('.tab-link').removeClass('active');
            $(parent).find(`.tab-link[data-bs-target="#${event.target.id}"]`).addClass('active');
            
            // reset body class
            $(parent).find('.tab-pane').removeClass('active');
            $(parent).find(`#${event.target.id}`).addClass('active');
        });

        // Move tabs to proper spot
        let tabWidgetContainers = $('.tab-widget-container:not(:has(.tab-widget-content))');
        let tabWidgetContents = $(':not(.tab-widget-container) > .tab-widget-content');
        let i;

        if (tabWidgetContainers && tabWidgetContents && tabWidgetContainers.length === tabWidgetContents.length) {
            for (i = 0; i < tabWidgetContainers.length; i += 1) {
                tabWidgetContainers[i].replaceChild(tabWidgetContents[i], tabWidgetContainers[i].children[0]);
            }
        }
        //--/ Move tabs to proper spot
    }
}

export default Tabs;

$(() => {
	const tabs = new Tabs();
	tabs.init();
});