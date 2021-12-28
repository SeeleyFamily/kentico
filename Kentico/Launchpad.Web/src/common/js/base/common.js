/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';

class Common {
    init() {
        this.initExternalLinks();
        this.smoothScrollLinks();
    }    

    initExternalLinks() {
		$('a:not(.open-self)').each((index, element) => {
            let a = new RegExp(`/${window.location.host}/`);

            if (!a.test(element.href) && element.href !== '') {
                $(element).click((event) => {
                    event.preventDefault();
                    event.stopPropagation();
                    window.open(element.href, '_blank');
                });
            }
        });
    }

    smoothScrollLinks() {
        $('a[href*="#"]:not([href="#"]):not(.js-inline-modal):not(.js-video):not(.image-modal)').click((e) => {
            if (
                window.location.hostname === e.currentTarget.hostname
                && e.currentTarget.pathname.replace(/^\//, '') === window.location.pathname.replace(/^\//, '')
            ) {
                let anchor = $(e.currentTarget.hash);
                anchor = anchor.length ? anchor : $(`[name="${e.currentTarget.hash.slice(1)}"]`);

                const headerHeight = $('header').height();
          
                if (anchor.length) {
                    $('html, body').animate({ scrollTop: anchor.offset().top - headerHeight }, 1000);
                }
            }
        });
    }
}

export default Common;