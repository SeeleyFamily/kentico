/*
 * Built with Common Launchpad 2.0.2
 */

import Vue from 'vue';
import Fragment from 'vue-fragment';
import VeeValidate from 'vee-validate'; // validation plugin
import { TooltipPlugin, AlertPlugin, CollapsePlugin, ModalPlugin } from 'bootstrap-vue';  // ui plugins
import store from '~src/common/vue/store';

Vue.use(Fragment.Plugin);

// init ui plugins add/remove as needed
Vue.use(AlertPlugin);
Vue.use(TooltipPlugin);
Vue.use(CollapsePlugin);
Vue.use(ModalPlugin);

// init validation plugin
Vue.use(VeeValidate, {
	events: 'change|blur',
	inject: false,
});

const dictionary = {
	en: {
		email: {
			required: () => 'Email address is required.'
		},
		phone: {
			required: () => 'Phone number is required.'
		},
	}
};

// Override and merge the dictionaries
VeeValidate.Validator.localize(dictionary);

// init app
const VueApp = new Vue({
    store,
});

export { VueApp as default };