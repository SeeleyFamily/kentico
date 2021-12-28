/*
 * Built with Common Launchpad 2.0.2
 */

import * as FormControl from '~/src/common/vue/utils/form-controls';
import * as CONST from '~/src/common/vue/utils/constants';

// global window
const filterObj = CONST.globalProject.ResourceFilters;
const filterDisplay = CONST.globalProject.ResourceFilterDisplay;

// initial state
// object name must match specification name
//specification, label, defaultValue, collapsible, expandDefault, groupType, subgroup, className
const filters = {
    searchTerm: new FormControl.FilterGroup(
        'SearchTerm', 'Filter By Keyword', '', false, true, 'textbox',
        new FormControl.Textbox('Type to search', 'SearchTerm', true), 'filter-search'
    ),
    Type: new FormControl.FilterGroup('Type', 'Filter by Type', null, false, true, 'checkbox', undefined, 'filter-flex filter-buttons-equal', filterDisplay.hideType),
    Topic: new FormControl.FilterGroup('Topic', 'Filter by Topic', null, false, true, 'checkbox', undefined, 'filter-flex', filterDisplay.hideTopic),
};

FormControl.assignFilters(filterObj, filters);

filters.Type.subgroup.unshift(new FormControl.SelectOption('999', 'All Types', true, false, false));
filters.Topic.subgroup.unshift(new FormControl.SelectOption('999', 'All Topics', true, false, false));

export { filters as default };