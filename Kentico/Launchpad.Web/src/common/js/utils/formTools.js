/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';
import 'bootstrap';
import 'bootstrap-select';
import Jodit from 'jodit';

import WysiwygConfig from './wysiwygConfig';
import youtubeParser from './youtubeparser';
import validationRules from './validation';
import * as CONST from './constants';
import Util from './ui';


class FormTools {
    wysiwyg(el) {
        const id = $(el).attr('id');
        const toolset = WysiwygConfig($(el).data('toolset'));
        const j = new Jodit(`#${id}`, toolset);
    }

    setDynamic(el) {
        if ($(el).hasClass('zipcode')) {
            this.zipcodeSetup(el);
        } else if ($(el).hasClass('email')) {
            this.emailSetup(el);
        } else if ($(el).hasClass('phone')) {
            this.phoneSetup(el);
        } else if ($(el).hasClass('required')) {
            $(el).blur(() => {
                validationRules.textValidate(el);
            });
        }
    }

    setTooltip(el, v) {
        const parent = $(el).closest('.form-field');
        const label = $(parent).find('label');
        const span = $('<span tabindex="0" class="tooltip"></span>');
        $(span).html(`?<div class="tooltip-box">${v}</div>`);
        if ($(label).length > 0) {
            $(label[0]).after(span);
        }
    }

    videoField(el) {
        this.setVideoThumb(el);
        $(el).blur(() => {
            this.setVideoThumb(el);
        });
    }

    zipcodeSetup(el) {
        if ($(el).hasClass('required')) {
            $(el).blur(() => {
                validationRules.zipValidate(el);
            });
        }
    }

    emailSetup(el) {
        if ($(el).hasClass('required')) {
            $(el).blur(() => {
                validationRules.emailValidate(el);
            });
        }
    }

    phoneSetup(el) {
        if ($(el).hasClass('required')) {
            $(el).blur(() => {
                validationRules.phoneValidate(el);
            });
        }
    }

    getVideoThumb(url) {
        if (url.indexOf('youtube') > -1 || url.indexOf('youtu.be') > -1) {
            const id = youtubeParser(url);
            if (id !== false) {
                return `https://img.youtube.com/vi/${id}/0.jpg`;
            }
        }
        return '';
    }

	setVideoThumb(el) {
        if ($(el).val() !== '') {
            const v = this.getVideoThumb($(el).val());
            $(`${el} + .video-preview`).html(`<img src='${v}'/>`);
        }
    }

	setupFileInput(sel) {
		if (sel) {
			$(sel).closest('.form-field').addClass('custom-file');
			$(sel).closest('.form-field').find('label').addClass('custom-file-label');
			$(sel).addClass('custom-file-input');
		}
    }

    selectRefresh(el) {
        $(el).selectpicker({
            mobile: true,
        });
    }

    validateForm(form) {
        let errors = 0;
        // test input fields
        let inputList = $(form).find('input');
        if (inputList.length > 0) {
            $(inputList).each((_, el) => {
                if ($(el).hasClass('required')) {
                    if ($(el).attr('type') === 'checkbox') {
                        errors += validationRules.checkboxValidate(el) ? 0 : 1;
                    } else if ($(el).attr('type') === 'radio') {
                        errors += validationRules.radioValidate(el) ? 0 : 1;
                    } else if ($(el).hasClass('zipcode')) {
                        errors += validationRules.zipValidate(el) ? 0 : 1;
                    } else if ($(el).hasClass('email')) {
                        errors += validationRules.emailValidate(el) ? 0 : 1;
                    } else if ($(el).hasClass('phone')) {
                        errors += validationRules.phoneValidate(el) ? 0 : 1;
                    } else {
                        errors += validationRules.textValidate(el) ? 0 : 1;
                    }
                }
            });
        }
        // test select fields
        let selectList = $(form).find('select');
        if (selectList.length > 0) {
            $(selectList).each((_, el) => {
                if ($(el).hasClass('required')) {
                    errors += validationRules.selectValidate(el) ? 0 : 1;
                }
            });
        }

        // test textarea fields
        let textAreaList = $(form).find('textarea');
        if (textAreaList.length > 0) {
            $(textAreaList).each((_, el) => {
                if ($(el).hasClass('required')) {
                    errors += validationRules.textValidate(el) ? 0 : 1;
                }
            });
        }
        return errors === 0;
    }

    overrideKenticoForm(event) {
        // run client side validation
        let success = window.FormTools.validateForm(event.target);
        if (success) {
            window.kentico.updatableFormHelper.submitFormOriginal(event);
                        
            if ($('.contact-form').length) {
                $('.contact-form .form-heading, .contact-form .form-note').fadeTo(500, 0);
            }
        } else {
            event.preventDefault();
            // scroll to errors
            let firstError = $('.error:visible, .field-validation-error12:visible')[0];
            let t = $(firstError).offset().top;
            let offset = (Util.getViewPort() !== CONST.ViewPort.Desktop) ? 200 : 350;
            $('html, body').animate({
                scrollTop: (t - offset)
            }, 2000);
        }
    }

    setupKenticoForm() {
        if (window.submitFormOriginal == null) {
            if (window.kentico && window.kentico.updatableFormHelper) {
                // override and replace the original kentico form submission
                window.kentico.updatableFormHelper.submitFormOriginal = window.kentico.updatableFormHelper.submitForm;
                window.kentico.updatableFormHelper.submitForm = this.overrideKenticoForm;
            }
        }
    }

    init() {
        $(document).ready(() => {
            this.setupKenticoForm();
        });
    }
}

export default FormTools;