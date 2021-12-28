/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import modaal from 'modaal';
import * as CONST from '../utils/constants';

class Modal {
    constructor() {
    }

    init() {
        this.bindEvents();
    }

    bindEvents() {
		$('.js-inline-modal').modaal();

		$('.js-video').modaal({
			type: 'video',
			width: 930
		});

		$('.image-modal').modaal({
			type: 'image',
			width: 930
		});
    }
}

export default Modal;

$(() => {
	const modal = new Modal();
	modal.init();
});