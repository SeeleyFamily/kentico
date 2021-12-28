/*
 * Built with Common Launchpad 2.0.2
 */

class LazyLoad {
    init() {
        let lazyloadImages;
        let lazyloadBgImages;

        let ioOptions = {
            rootMargin: '0px 0px 400px 0px',
        };

        // Images
        if ('IntersectionObserver' in window) {
            lazyloadImages = document.querySelectorAll('.js-lazy');

            let imageObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    if (entry.isIntersecting) {
                        let image = entry.target;

                        if (image.classList.contains('js-webp')) {
                            let imageSrc = '';
                            if (document.querySelector('html').classList.contains('webp')) {
                                imageSrc = image.getAttribute('data-image-webp');
                            }
                            else {
                                imageSrc = image.getAttribute('data-image');
                            }

                            image.src = imageSrc;
                        }
                     
                        image.classList.remove('lazy');
                        imageObserver.unobserve(image);
                    }
                });
            }, ioOptions);

            lazyloadImages.forEach((image) => {
                imageObserver.observe(image);
            });
        }
        else {
            // IE11 Hack, force scrolling
            if (!!window.MSInputMethodContext && !!document.documentMode) {
                window.scrollTo(0, 1);
            }

            document.addEventListener('scroll', this.imgLazyLoad);
            window.addEventListener('resize', this.imgLazyLoad);
            window.addEventListener('orientationChange', this.imgLazyLoad);
        }
        //---/ End Images

        // Backgrounds   
        if ('IntersectionObserver' in window) {
            lazyloadBgImages = document.querySelectorAll('.js-lazy-bg');

            let bgImageObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    if (entry.isIntersecting) {
                        let image = entry.target;
                        if (image.classList.contains('js-webp-bg')) {
                            let background = '';
                            if (document.querySelector('html').classList.contains('webp')) {
                                background = image.getAttribute('data-bg-webp');
                                if (!background) {
                                    background = image.getAttribute('data-image-webp');
                                }
                            }
                            else {
                                background = image.getAttribute('data-bg');
                                if (!background) {
                                    background = image.getAttribute('data-image');
                                }
                            }
                            if (background) {
                                image.setAttribute('style', `background-image: url('${background}');`);
                            }
                        }
                        image.classList.remove('lazy-bg');
                        bgImageObserver.unobserve(image);
                    }
                });
            }, ioOptions);

            lazyloadBgImages.forEach((image) => {
                bgImageObserver.observe(image);
            });
        }
        else {
            // IE11 Hack, force scrolling
            if (!!window.MSInputMethodContext && !!document.documentMode) {
                window.scrollTo(0, 1);
            }

            document.addEventListener('scroll', this.bgLazyload);
            window.addEventListener('resize', this.bgLazyload);
            window.addEventListener('orientationChange', this.bgLazyload);
        }
        //--/ End Backgrounds
    }

    imgLazyLoad() {
        let lazyloadThrottleTimeout;
        let lazyloadImages = Array.prototype.slice.call(document.querySelectorAll('.js-lazy'));

        if (lazyloadThrottleTimeout) {
            clearTimeout(lazyloadThrottleTimeout);
        }    

        lazyloadThrottleTimeout = setTimeout(() => {
            let scrollTop = window.pageYOffset;
            lazyloadImages.forEach((img) => {
                if (img.offsetTop < (window.innerHeight + scrollTop + 100)) {
                    img.src = img.dataset.image;
                    img.classList.remove('js-lazy');
                }
            });

            if (lazyloadImages.length === 0) { 
                document.removeEventListener('scroll', this.imgLazyLoad);
                window.removeEventListener('resize', this.imgLazyLoad);
                window.removeEventListener('orientationChange', this.imgLazyLoad);
            }
        }, 20);
    }

    bgLazyload() {
        let lazyloadThrottleTimeout;
        let lazyloadBgImages = Array.prototype.slice.call(document.querySelectorAll('.js-lazy-bg'));

        if (lazyloadThrottleTimeout) {
            clearTimeout(lazyloadThrottleTimeout);
        }    

        lazyloadThrottleTimeout = setTimeout(() => {
            let scrollTop = window.pageYOffset;
            lazyloadBgImages.forEach((img) => {
                if (img.offsetTop < (window.innerHeight + scrollTop + 100)) {
                    img.style.backgroundImage = `url('${img.dataset.bg}')`;
                    img.classList.remove('js-lazy-bg');
                }
            });

            if (lazyloadBgImages.length === 0) { 
                document.removeEventListener('scroll', this.bgLazyload);
                window.removeEventListener('resize', this.bgLazyload);
                window.removeEventListener('orientationChange', this.bgLazyload);
            }
        }, 20);
    }
}

export default LazyLoad;

$(() => {
	const lazyload = new LazyLoad();
	lazyload.init();
});