/*
 * Built with Common Launchpad 2.0.2
 */

import * as FormControl from '../../common/utils/form-controls';
import * as CONST from '../../common/utils/constants';

// global window
const filterObj = CONST.globalProject.ResourceFilters;

// initial state
// object name must match specification name
//specification, label, defaultValue, collapsible, expandDefault, groupType, subgroup, className
const filters = {
    searchTerm: new FormControl.FilterGroup(
        'SearchTerm', 'Filter By Keyword', '', false, true, 'textbox',
        new FormControl.Textbox('Type to search', 'SearchTerm', true), 'filter-search'
    ),
    Topic: new FormControl.FilterGroup('Topic', 'Filter by Topic', null, false, true, 'select', undefined, 'filter-flex'),
};

FormControl.assignFilters(filterObj, filters);

export { filters as default };