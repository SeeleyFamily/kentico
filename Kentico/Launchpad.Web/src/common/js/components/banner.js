/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import Cookies from 'js-cookie';
import * as CONST from '../utils/constants';
import Util from '../utils/ui';

class Banner {
    constructor() {}

    init() {
        this.bindEvents();
    }

    bindEvents() { 
        $(document).on('click keypress', '.js-banner-close', e => {
            const createCookie = $(e.currentTarget).data('create-cookie');
            const bannerId = $(e.currentTarget).data('banner-id');
            const expirationDays = $(e.currentTarget).data('expiration-days');

			this.closeBanner($(e.currentTarget));

            // Check if we should create a cookie or not
            if (createCookie) {
                // Check if cookie exists
                if (Cookies.get('lp.banners') && Cookies.get('lp.banners').length) {
                    // Update cookie list
                    const bannerIdList = [Cookies.get('lp.banners'), bannerId].join(',');
                    Cookies.set('lp.banners', bannerIdList, { expires: expirationDays });
                }
                else {
                    // Create cookie
                    Cookies.set('lp.banners', bannerId, { expires: expirationDays });
                }
            }
        });
    }

    closeBanner(e) {
        $(e).closest('.banner').slideToggle();
        $(e).closest('.banner').toggleClass('closed');
    }
}

export default Banner;

$(() => {
	const banner = new Banner();
	banner.init();
});