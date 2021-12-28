/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import Amplitude from 'amplitudejs';
import * as CONST from '../utils/constants';
import Util from '../utils/ui';

class AudioPlayer {
    constructor() {}

    init() {
		this.bindEvents();
		this.initAudioPlayer();
    }

    bindEvents() { 
        
	}

	initAudioPlayer() {
		let songs = [];

		$('.js-player').each((index, player) => {
			let id = $(player).data('id');
			let url = $(player).data('url');
			let title = $(player).data('title');
			let subtitle = $(player).data('subtitle');

			songs.push({
				name: title,
				album: subtitle,
				url
			});


			$(`#${id}`).on('click', (e) => {
				if (Amplitude.getActiveIndex() === index) {
					let offset = e.currentTarget.getBoundingClientRect();
					let x = e.pageX - offset.left;

					Amplitude.setSongPlayedPercentage((parseFloat(x) / parseFloat(e.currentTarget.offsetWidth)) * 100);
				}
			});

			$(player).find('[data-amplitude-song-index]').each((k, v) => {
				$(v).attr('data-amplitude-song-index', index);
			});
		});


		Amplitude.init({
			songs
		});
	}
}

export default AudioPlayer;

$(() => {
	const audioplayer = new AudioPlayer();
	audioplayer.init();
});