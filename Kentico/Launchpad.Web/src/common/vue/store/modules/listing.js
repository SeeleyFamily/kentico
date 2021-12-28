/*
 * Built with Common Launchpad 2.0.2
 */

import { getField, updateField } from 'vuex-map-fields';
import listing from '../../api/listing';

// initial state
const state = {
	error: '',
	loading: false,
	listing: [],
	resource: '', // api url for specific page listing
	specification: {},
	pagination: {
		pageIndex: 0,
		pageSize: 0,
		rowEnd: 0,
		rowStart: 0,
		total: 0,
		totalPages: 0,
	},
	initState: {},
};

// getters
const getters = {
	getField,
};

// actions
const actions = {
	updateError({ commit, rootState }, error) {
		commit('updateError', error);
	},
	updateLoading({ commit, rootState }, loading) {
		commit('updateLoading', loading);
	},
	setFilter({ commit, rootState }) {
		// call the api with update specification object
		listing.setFilter(data => {
			commit('setListing', data);
		}, { specification: state.specification, resource: state.resource });
	},
	setPeopleProfileFilter({ commit, rootState }) {
		// call the api with update specification object
		listing.setFilter(data => {
			commit('setPeopleProfileListing', data);
		}, { specification: state.specification, resource: state.resource });
	},
};

// mutations
const mutations = {
	updateField,
	updateError(state, error) {
		state.error = error;
	},
	updateLoading(state, loading) {
		state.loading = loading;
	},
	setInitData(state, result) {
		// on load of app, load in data
        state.initState = result;
        state.listing = result.Items;
        state.specification = { ...state.initState.Specification };

		state.pagination.pageIndex = result.PageIndex + 1;
		state.pagination.pageSize = result.PageSize;
		state.pagination.rowEnd = result.RowEnd;
		state.pagination.rowStart = result.RowStart;
		state.pagination.total = result.Total;
		state.pagination.totalPages = result.TotalPages;
	},
	setResource(state, resource) {
		state.resource = resource;
	},
    updateSpecification(state, param) {
		// on each input change, update the specification object
		for (const [key, value] of Object.entries(state.specification)) {
			if (param.key === key && param.value != null) {
				state.specification[key] = param.value;
			}
		}

		// when executing any filter outside of paging, set paging to 0
		if (param.key !== 'PageIndex') {
			state.specification.PageIndex = 0;
            state.pagination.pageIndex = 1;
            if (state.specification.Skip !== undefined) {
                state.specification.Skip = 0;
            }
		}
		else {
			state.pagination.pageIndex = param.value + 1;
        }

        // If PageIndex = 0, set Skip to 0
        if (param.key === 'PageIndex') {
            if (param.value === 0) {
                if (state.specification.Skip !== undefined) {
                    state.specification.Skip = 0;
                }
            }
        }

        // If Sort, set Skip to 0
        if (param.key === 'Sort') {
            if (state.specification.Skip !== undefined) {
                state.specification.Skip = 0;
            }
        }

        // Update url parameters
        if (window.history.pushState) {
            // Encode data for url parameters
            const encodeDataToURL = (data) => {
                const value = Object
                    .keys(data)
                    .map(value => `${value}=${encodeURIComponent(data[value])}`)
                    .join('&');

                return value;
            };

            // Function to remove null or empty values
            const removeEmpty = (obj) => {
                const newObj = [];

                Object.keys(obj).forEach(key => {
                    if (obj[key] && typeof obj[key] === 'object') {
                        newObj[key] = removeEmpty(obj[key]);
                    }
                    else if (obj[key] !== null && obj[key] !== '') {
                        if (obj[key] !== '999') {
                            newObj[key] = obj[key];
                        }
                    }
                });

                return newObj;
            };

            // Remove null items
            const specification = removeEmpty(state.specification);

            // Create URL
            let newurl = `${window.location.protocol}//${window.location.host}${window.location.pathname}?${encodeDataToURL(specification)}`;

            // Update URL
            window.history.pushState({ path: newurl }, '', newurl);
        }
        //---/ Update URL parameters
	},
	updatePeopleProfileSpecification(state, param) {
		// on each input change, update the specification object
		if (param.value != null) {
			state.specification[param.key] = param.value;
		}

		// when executing any filter outside of paging, set paging to 0
		if (param.key !== 'PageIndex') {
			state.specification.PageIndex = 0;
			state.pagination.pageIndex = 1;
			if (state.specification.Skip !== undefined) {
				state.specification.Skip = 0;
			}
		}
		else {
			state.pagination.pageIndex = param.value + 1;
		}
	},
	resetSpecification(state) {
		// reset listing and filters back to their initial on load state
		state.listing = state.initState.Items;
		state.specification = state.initState.Specification;

		state.pagination.pageIndex = state.initState.PageIndex + 1;
		state.pagination.pageSize = state.initState.PageSize;
		state.pagination.rowEnd = state.initState.RowEnd;
		state.pagination.rowStart = state.initState.RowStart;
		state.pagination.total = state.initState.Total;
        state.pagination.totalPages = state.initState.TotalPages;

        // Reset URL parameters
        // Create URL
        let newurl = `${window.location.protocol}//${window.location.host}${window.location.pathname}`;

        // Update URL
        window.history.pushState({ path: newurl }, '', newurl);
	},
	setListing(state, data) {
		// update listing and pagination after api call
		state.listing = data.Items;
		state.specification = data.Specification;

		state.pagination.pageIndex = data.PageIndex + 1;
		state.pagination.pageSize = data.PageSize;
		state.pagination.rowEnd = data.RowEnd;
		state.pagination.rowStart = data.RowStart;
		state.pagination.total = data.Total;
		state.pagination.totalPages = data.TotalPages;
	},
	setPeopleProfileListing(state, data) {
		// update listing and pagination after api call
		state.listing = [...state.listing, ...data.Items];
		state.specification = data.Specification;

		state.pagination.pageIndex = data.PageIndex + 1;
		state.pagination.pageSize = data.PageSize;
		state.pagination.rowEnd = data.RowEnd;
		state.pagination.rowStart = data.RowStart;
		state.pagination.total = data.Total;
		state.pagination.totalPages = data.TotalPages;
	}
};

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations
};
