/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';

function displayError(el, type) {
	$(el).siblings(`.error.${type}`).removeClass('hidden');
	$(el).addClass('error-input');
}

function displaySuccess(el) {
    $(el).siblings('.error').addClass('hidden');
    $(el).siblings('.field-validation-error').addClass('hidden');
	$(el).removeClass('error-input');
}

function isEmpty(el) {
    if ($(el).val() === '') {
        return true;
    } else {
        return false;
    }
}

function isValidUSZip(sZip) {
    return /^\d{5}(-\d{4})?$/.test(sZip);
}

function isValidEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function isValidPhone(phone) {
    const re = /^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$/;
    return re.test(String(phone));
}

const textValidate = (el) => {
    if (isEmpty(el)) {
        displayError(el, 'empty');
        return 0;
    } else {
        displaySuccess(el);
    }
    return 1;
};

const zipValidate = (el) => {
    if (isEmpty(el) || !isValidUSZip($(el).val())) {
        displayError(el, 'zipcode');
        return 0;
    } else {
        displaySuccess(el);
    }
    return 1;
};

const emailValidate = (el) => {
    if (isEmpty(el) || !isValidEmail($(el).val())) {
        displayError(el, 'email');
        return 0;
    } else {
        displaySuccess(el);
    }
    return 1;
};


const phoneValidate = (el) => {
    if (isEmpty(el) || !isValidPhone($(el).val())) {
        displayError(el, 'phone');
        return 0;
    } else {
        displaySuccess(el);
    }
    return 1;
};

const selectValidate = (el) => {
    if (isEmpty(el)) {
        let customParent = $(el).parent('.bootstrap-select');
        if (customParent.length > 0) {
            displayError(customParent, 'empty');
        } else {
            displayError(el, 'empty');
        }
        return 0;
    }
    return 1;
};

const checkboxValidate = (el) => {
    let name = $(el).attr('name');
    let checked = $(`input[name='${name}']:checked`);
    if (checked.length === 0) {
        let parent = $(el).closest('.checkbox-list');
        displayError(parent, 'empty');
        return 0;
    }
    return 1;
};


const radioValidate = (el) => {
    let name = $(el).attr('name');
    let checked = $(`input[name='${name}']:checked`);
    if (checked.length === 0) {
        let parent = $(el).closest('.radio-container');
        displayError(parent, 'empty');
        return 0;
    }
    return 1;
};

export default { 
    textValidate,
    zipValidate,
    emailValidate,
    phoneValidate,
    selectValidate,
    checkboxValidate,
    radioValidate
};