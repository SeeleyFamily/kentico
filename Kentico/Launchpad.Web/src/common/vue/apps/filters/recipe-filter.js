/*
 * Built with Common Launchpad 2.0.2
 */

import * as FormControl from '../../common/utils/form-controls';
import * as CONST from '../../common/utils/constants';

// global window
const filterObj = CONST.globalProject.RecipeFilters;

// initial state
// object name must match specification name
//specification, label, defaultValue, collapsible, expandDefault, groupType, subgroup, className
const filters = {
    Sort: new FormControl.FilterGroup('Sort', 'Sort By', 'Newest', false, true, 'select'),
    searchTerm: new FormControl.FilterGroup(
        'SearchTerm', 'Filter By Keyword', '', false, true, 'textbox',
        new FormControl.Textbox('Type to search', 'SearchTerm', true), 'filter-search'
    ),
    RecipeType: new FormControl.FilterGroup('RecipeType', 'Filter by Type', null, false, true, 'checkbox', undefined, 'filter-flex filter-buttons-equal'),
};

FormControl.assignFilters(filterObj, filters);

filters.RecipeType.subgroup.unshift(new FormControl.SelectOption('999', 'All Types', true, false, false));

export { filters as default };