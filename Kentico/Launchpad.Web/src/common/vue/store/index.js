/*
 * Built with Common Launchpad 2.0.2
 */

import Vue from 'vue';
import Vuex from 'vuex';
import listing from './modules/listing';

Vue.use(Vuex);

const debug = process.env.NODE_ENV !== 'production';

export default new Vuex.Store({
	modules: {
		listing,
	},
	strict: debug,
});
