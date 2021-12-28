/*
 * Built with Common Launchpad 2.0.2
 */

import * as FormControl from '../../common/utils/form-controls';
import * as CONST from '../../common/utils/constants';

// global window
const filterObj = CONST.globalProject.EventFilters;

// initial state
// object name must match specification name
//specification, label, defaultValue, collapsible, expandDefault, groupType, subgroup, className
const filters = {
    Sort: new FormControl.FilterGroup('Sort', 'Sort By', 'MostRelevant', false, true, 'select'),
    Query: new FormControl.FilterGroup(
        'Query', 'Filter By Keyword', '', false, true, 'textbox',
        new FormControl.Textbox('Type to search', 'Query', true), 'filter-search'
    ),
    Types: new FormControl.FilterGroup('Types', 'Filter by Type', 'All Topics', false, true, 'checkbox', undefined, 'filter-flex'),
    Topics: new FormControl.FilterGroup('Topics', 'Filter by Topic', 'All Types', false, true, 'checkbox', undefined, 'filter-flex')
};

FormControl.assignFilters(filterObj, filters);

filters.Types.subgroup.unshift(new FormControl.SelectOption('999', 'All Types', true, false, false));
filters.Topics.subgroup.unshift(new FormControl.SelectOption('999', 'All Topics', true, false, false));

export { filters as default };