/*
 * Built with Common Launchpad 2.0.2
 */

export function FilterGroup(specification, label, defaultValue, collapsible, expandDefault, groupType, subgroup, className, hideFilter) {
	this.label = label;
	this.collapsible = collapsible;
	this.expandDefault = expandDefault;
	this.specification = specification;
	this.groupType = groupType;
    this.defaultValue = defaultValue;
    this.className = className;

    if (hideFilter !== undefined) {
        this.hideFilter = hideFilter;
    }
    else {
        this.hideFilter = 'False';
    }

	if (subgroup !== undefined) {
		this.subgroup = subgroup;
	}
	else if (groupType === 'checkbox' || groupType === 'radio') {
		this.subgroup = [];
	}
	else {
		this.subgroup = {};
	}
}

export function Textbox(placeholder, specification, searchButton) {
    this.placeholder = placeholder;
    this.specification = specification;
    this.searchButton = searchButton;
}

export function Select(placeholder, defaultValue) {
    this.placeholder = placeholder;
    this.defaultValue = defaultValue;
}

// checkbox/radio options
export function SelectOption(value, label, selected, collapsible, expandDefault) {
	this.value = value;
	this.label = label;
	this.selected = selected;
	this.collapsible = collapsible;
	this.expandDefault = expandDefault;
	this.subgroup = [];
}

// loop through checkbox/radio children and create objects
function createSelectOption(option, parent) {
	let collapsible = parent.collapsible && option.Options !== null;
	let selectOption = new SelectOption(option.Value, option.Name, false, collapsible, parent.expandDefault, null);

	parent.subgroup.push(selectOption);

	if (option.Options != null) {
		for (let suboption of option.Options) {
			createSelectOption(suboption, selectOption);
		}
	}
}

export function assignFilters(filterObj, filters) {
	// loop through filters from back-end and assign to js objects
	for (let filter of filterObj) {
		if (filter.Specification === filters[filter.Specification].specification) {
			if (filter.Label !== '' && filters[filter.Specification].label === '') {
                filters[filter.Specification].label = filter.Label;
			}

            if (filters[filter.Specification].groupType === 'select') {
				// if select, assign name/value paris to object directly
                filters[filter.Specification].subgroup = filter.Options;
			}
			else if (filters[filter.Specification].groupType === 'checkbox' || filters[filter.Specification].groupType === 'radio') {
				// if a radio/checkbox list, loop through and create respective objects
				for (let option of filter.Options) {
					let collapsible = filters[filter.Specification].collapsible && option.Options !== null;
					let selectOption = new SelectOption(option.Value, option.Name, false, collapsible, filters[filter.Specification].expandDefault, null);

					filters[filter.Specification].subgroup.push(selectOption);

					if (option.Options !== null) {
						for (let suboption of option.Options) {
							createSelectOption(suboption, selectOption);
						}
					}
				}
			}
		}
	}
}
