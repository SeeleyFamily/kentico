/*
 * Built with Common Launchpad 2.0.2
 */

// JS
import '~/src/common/js/index';

// Base SCSS
import '~/src/common/scss/base.scss';
import '~/src/common/scss/layout.scss';

// Vendor CSS
import 'simplebar/dist/simplebar.css';

// Components SCSS
import '~/src/common/scss/components/filter/filter-header.scss';
import '~/src/common/scss/components/filter/filter-sidebar.scss';
import '~/src/common/scss/components/pagination/pagination.scss';
import '~/src/common/scss/components/content-listing/content-listing.scss';


// import Vue App
import Vue from 'vue';
import VueApp from '~/src/common/vue/apps/list-filter';
import App from '~/src/common/vue/apps/containers/content-list-filter-app.vue';

// connect vue component and init app
Vue.component('app', App);
VueApp.$mount('#filter-app');
