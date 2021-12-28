<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<aside class="sidebar filter-sidebar">
		<div class="filter-title js-accordion-title">{{ title }}</div>
		<div class="filter-content js-accordion-content">
			<filter-group v-for="(filter, key, index) in filters" v-bind:filter="filter" v-bind:key="key" v-bind:index="index" v-bind:specification="filter.specification"></filter-group>
		</div>
	</aside>
</template>

<script>
	import { mapState, mapActions, mapMutations } from 'vuex';
	import FilterGroup from '../ui/filter-group';

	export default {
		props: ['filters', 'title'],
		data: function() {
			return  {
				query: '',
				pageSize: 5
			}
		},
		computed: mapState({
			pagination: state => state.listing.pagination,
			specification: state => state.listing.specification,
		}),
		components: {
			'filter-group': FilterGroup,
		},
		mounted() {
			this.query = this.specification.SearchTerm;
		},
		methods: {
			...mapActions('listing', { setFilter: 'setFilter' }),
			...mapMutations('listing', { updateSpecification: 'updateSpecification' }),
			...mapMutations('listing', { resetSpecification: 'resetSpecification' }),
			setSearchTerm() {
				this.updateSpecification({ key: 'SearchTerm', value: decodeURIComponent(this.query) });
			},
			setPageSize() {
				this.updateSpecification({ key: 'PageSize', value: this.pageSize });
			},
			filter() {
				this.setFilter();
			},
			resetFilter() {
				this.resetSpecification();
			}
        },
    }
</script>