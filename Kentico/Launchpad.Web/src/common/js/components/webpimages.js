/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';

class WebPImages {
    init() {
       let observer = new MutationObserver((mutations) => {
            mutations.forEach((mutation) => {
                if (mutation.addedNodes && mutation.addedNodes.length > 0) {
                   this.setWebPImages();
                }
            });
        });

        let config = {
            attributes: true,
            childList: true,
            characterData: true
        };

        observer.observe(document.body, config);        

        this.setWebPImages();
    }

    setWebPImages() {
        if ($('.js-webp-bg:not(.js-webp-bg-loaded)').length) {
            $('.js-webp-bg:not(.js-webp-bg-loaded)').each((index, element) => {
                let backgroundImage = '';
                let image = $(element);

                if (image.hasClass('js-lazy-bg') === false) {
                    if ($('html').hasClass('webp')) {
                        backgroundImage = image.data('bg-webp');
                        if (!backgroundImage) {
                            backgroundImage = image.data('image-webp');
                        }
                    }
                    else {
                        backgroundImage = image.data('image');
                        if (!backgroundImage) {
                            backgroundImage = image.data('bg');
                        }
                    }
                }
                if (backgroundImage) {
                    image.attr('style', `background-image: url('${backgroundImage}');`);
                }
                image.addClass('js-webp-bg-loaded');
            });
        }
    }
}

export default WebPImages;

$(() => {
	const webpimages = new WebPImages();
	webpimages.init();
});