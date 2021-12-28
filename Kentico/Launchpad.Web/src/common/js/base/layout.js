/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import * as CONST from '../utils/constants';
import Util from '../utils/ui';

class Layout {
	constructor() {
		this.$body = $('body');
		this.$header = $('.header');
		this.$hiddenHeader = $('.js-top-target');

		this.$nav = $('.nav-wrapper');
		this.$primaryNavItem = $('.primary-nav > li');
		this.$primaryNavItemLink = $('.primary-nav > li > a');
        this.$navToggle = $('.js-nav-toggle');
        this.$navToggleLabel = $('.js-nav-toggle-label');
		this.$navToggleChildren = $('.js-nav-toggle-children');

		this.$headerSearchTextbox = $('.js-search-textbox');

		this.didScroll = false;
		this.lastScrollTop = 0;
		this.delta = 5;

		this.$footerAccordionItem = $('.js-footer-accordion > .footer-column-navigation-item--parent > a');

        this.timer = 0;

        this.$menuButton = $('.js-menu-icon');
        this.$menuIcon = '<svg aria-hidden="true" data-prefix="far" data-icon="bars" class="svg-inline--fa fa-bars fa-w-14" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path fill="currentColor" d="M436 124H12c-6.627 0-12-5.373-12-12V80c0-6.627 5.373-12 12-12h424c6.627 0 12 5.373 12 12v32c0 6.627-5.373 12-12 12zm0 160H12c-6.627 0-12-5.373-12-12v-32c0-6.627 5.373-12 12-12h424c6.627 0 12 5.373 12 12v32c0 6.627-5.373 12-12 12zm0 160H12c-6.627 0-12-5.373-12-12v-32c0-6.627 5.373-12 12-12h424c6.627 0 12 5.373 12 12v32c0 6.627-5.373 12-12 12z"/></svg>';
        this.$closeIcon = '<svg aria-hidden="true" data-prefix="far" data-icon="times" class="svg-inline--fa fa-times fa-w-10" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512"><path fill="currentColor" d="M207.6 256l107.72-107.72c6.23-6.23 6.23-16.34 0-22.58l-25.03-25.03c-6.23-6.23-16.34-6.23-22.58 0L160 208.4 52.28 100.68c-6.23-6.23-16.34-6.23-22.58 0L4.68 125.7c-6.23 6.23-6.23 16.34 0 22.58L112.4 256 4.68 363.72c-6.23 6.23-6.23 16.34 0 22.58l25.03 25.03c6.23 6.23 16.34 6.23 22.58 0L160 303.6l107.72 107.72c6.23 6.23 16.34 6.23 22.58 0l25.03-25.03c6.23-6.23 6.23-16.34 0-22.58L207.6 256z"/></svg>';
    }

    init() {
        this.bindEvents();
    }

    bindEvents() {
		this.$navToggle.on('click', e => { e.preventDefault(); this.toggleNav(e); });

		this.$navToggleChildren.on('click', e => { e.preventDefault(); this.mobileNavItem(e); });

		// on focus header search
		this.$headerSearchTextbox.on('focus', e => { this.expandSearch(e); });
		$('.header-search-prepend-icon').on('click', e => { $('#header-search').focus(); this.expandSearch(e); });

		$('body').on('click', e => { this.collapseSearch(e); });

		$('.header .search').on('click', e => { e.stopPropagation(); });

		// on menu enter
		this.$primaryNavItemLink.on('keydown', e => { this.tabbingNav(e); });

        this.$footerAccordionItem.on('click', e => { e.preventDefault(); this.footerAccordion(e); });

		// open megamenu on hover for desktop
		this.$primaryNavItem.on('mouseover', e => {
			clearTimeout(this.timer);
			$('.js-nav-megamenu').removeClass('open');

			this.openSubmenu(e);
		}).on('mouseleave', e => {
			this.timer = setTimeout(() => {
				this.closeSubmenu(e);
			}, 300);
		});

		// Hide Header on on scroll down
		$(window).scroll(e => {
			this.didScroll = true;
		});

		setInterval(() => {
			if (this.didScroll) {
				this.hasScrolled();
				this.didScroll = false;
			}
        }, 100);
		
		//Scrolls back to the top after tabbing through page
		
		this.$hiddenHeader.focus(() => {
			window.scroll({
				top: 0, 
				left: 0, 
				behavior: 'smooth'
			});
		});
        //---/ Footer
	}

	closeSubmenu(e) {
		const target = $(e.currentTarget);

		$(target).closest('li').find('.js-nav-megamenu').removeClass('open');
	}

	expandSearch(e) {
		if (Util.getViewPort() === CONST.ViewPort.Desktop) {
			let target = e.currentTarget;
			let parent = $(target).closest('.search');

			$(parent).addClass('focused');
			$(target).val($(target).data('value'));
		}
	}

	collapseSearch(e) {
		if ($('.header .search').hasClass('focused') && !$('#header-search').is(':focus')) {
			$('.header .search').removeClass('focused');
			$('#header-search').data('value', $('.header .search .form-control').val());  
			$('#header-search').val(''); 
		} 
	}

	hasScrolled() {
		let st = $(window).scrollTop();
		let headerHeight = $('.header').outerHeight();
		let bannerHeight = ($('.js-banner').hasClass('closed') || $('.js-banner') === undefined || $('.js-banner').length === 0) ? 0 : $('.js-banner').outerHeight();

		// Make sure they scroll more than delta
		if (Math.abs(this.lastScrollTop - st) <= this.delta) {
			return;
		}

		// If they scrolled down and are past the header, add class .header-up.
		// This is necessary so you never see what is "behind" the header.
		if (st > headerHeight) {
			this.$body.css('padding-top', headerHeight);

			if (st > this.lastScrollTop) {
				// Scroll Down
				this.$body.removeClass('header-down').addClass('header-up');
				this.$header.css('top', -headerHeight);
			}
			else if (st + $(window).height() < $(document).height()) {
				// Scroll Up
				this.$body.removeClass('header-up').addClass('header-down');
				this.$header.css('top', 0 - bannerHeight);
			}
		}
		else if (st <= (bannerHeight + 50)) {
			this.$body.css('padding-top', 0);
			this.$header.css('top', 'auto');
			this.$body.removeClass('header-up').removeClass('header-down');
		}

		this.lastScrollTop = st;
	}

	// clicking on items in mobile nav
	mobileNavItem(e) {
		if (Util.getViewPort() !== CONST.ViewPort.Desktop) {
			e.preventDefault();

			const target = $(e.currentTarget);
			const parent = target.closest('li');

			$(parent).find('> ul').slideToggle();
			$(parent).toggleClass('open');
		}
	}

	openSubmenu(e) {
		const target = $(e.currentTarget);

		$(target).closest('li').find('.js-nav-megamenu').addClass('open');
	}

	// if user hits enter when tabbing on menu items, open mega menu and set tabbing to children
	tabbingNav(e) {
		let target = $(e.currentTarget);

		if (e.keyCode === 13 && target.hasClass('has-dropdown')) {
			e.preventDefault();
			target.closest('li').addClass('focused');
			target.closest('li').find('.js-nav-megamenu').addClass('open');
		}

		let $activeElem = $(document.activeElement);
		let $prevItem = $activeElem.closest('li').prev();

		if ($prevItem.hasClass('focused')) {
			$prevItem.removeClass('focused');
			$prevItem.find('.js-nav-megamenu').removeClass('open');
		}

		if (e.shiftKey && (e.keyCode === 9)) {
			$('.js-nav-megamenu').removeClass('open');
		}
	}

	// toggle nav on mobile
	toggleNav(e) {
		$('.js-nav-megamenu').removeAttr('style').closest('li').removeClass('open'); // Open nav with sub menus closed

		$('.js-nav-toggle').toggleClass('open');

		this.$nav.slideToggle().toggleClass('open');
        this.$body.toggleClass('nav-open');
	}

    // Footer accordion expand/collapse
    footerAccordion(e) {
        const target = $(e.currentTarget);

		if (Util.getViewPort() !== CONST.ViewPort.Desktop) {
            target.parent().toggleClass('open');
			target.siblings('.footer-column-navigation-sub').slideToggle();
        }
		else {
			let href = $(target).attr('href');
			window.location = href;
		}        
    }
}

export default Layout;