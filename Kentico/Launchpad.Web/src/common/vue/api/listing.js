/*
 * Built with Common Launchpad 2.0.2
 */

import axios from 'axios';
import * as CONST from '../utils/constants';

export default {
	setFilter(cb, { specification, resource }) {
		let query = '';

        for (let [key, value] of Object.entries(specification)) {
			if (value !== null) {
                if (Array.isArray(value)) {
					// if array, loop through value
                    for (let val of value) {
                        if (val !== null && val !== '999') {
                            query += `${key}=${val}&`;
                        }
					}
				}
				else {
					query += `${key}=${value}&`;
				}	
			}
		}

        query = query.slice(0, -1);

		axios.get(`${CONST.EnvironmentUrl}${resource}?${query}`, {
				headers: {
					'Content-Type': 'application/json'
				}
			})
			.then(response => response.data)
			.then(data => {
				cb(data);
			})
			.catch(err => cb(err.response));
	},
};