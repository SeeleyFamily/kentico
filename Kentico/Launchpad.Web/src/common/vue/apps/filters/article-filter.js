/*
 * Built with Common Launchpad 2.0.2
 */

import * as FormControl from '../../common/utils/form-controls';
import * as CONST from '../../common/utils/constants';

// global window
const filterObj = CONST.globalProject.ArticleFilters;

// initial state
// object name must match specification name
const filters = {
	searchTerm: new FormControl.FilterGroup(
		'SearchTerm', '', null, false, true, 'textbox',
		new FormControl.Textbox('Search Term', 'Search Term', '')
	),
	IsLaunchpadCertified: new FormControl.FilterGroup('IsLaunchpadCertified', '', null, false, true, 'radio'),
	ServiceLine: new FormControl.FilterGroup('ServiceLine', 'Services', null, true, true, 'checkbox'),
	Industry: new FormControl.FilterGroup('Industry', '', null, false, true, 'checkbox'),
	CustomTableReference: new FormControl.FilterGroup('CustomTableReference', '', 'Select One', false, true, 'select'),
	Sort: new FormControl.FilterGroup('Sort', '', 'Sort By', false, true, 'select'),
};

FormControl.assignFilters(filterObj, filters);

export { filters as default };