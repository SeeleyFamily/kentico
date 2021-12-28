/*
 * Built with Common Launchpad 2.0.2
 */

import $ from 'jquery';

import Common from './base/common';
import Layout from './base/layout';

// Shared Components
import './components/banner';
import './components/webpimages';
import './components/lazyload';

$(() => {
	const common = new Common();
    common.init();

	const layout = new Layout();
    layout.init();
});
