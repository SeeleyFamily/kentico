/*
 * Built with Common Launchpad 2.0.2
 */

import * as FormControl from '../../common/utils/form-controls';
import * as CONST from '../../common/utils/constants';

// global window
const filterObj = CONST.globalProject.SearchFilters;

// initial state
// object name must match specification name
const filters = {
	Query: new FormControl.FilterGroup(
		'Query', '', CONST.globalProject.Query, false, true, 'textbox',
		new FormControl.Textbox('Search Term', 'Query', true), 'filter-search'
	),
};

FormControl.assignFilters(filterObj, filters);

export { filters as default };