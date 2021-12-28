<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<section class="content-hero">
		<div class="container">
			<div class="content-hero-content">
				<div class="content-hero-txt">
					<h1>Search Results</h1>
					<div class="search-input">
						<filter-group v-for="(filter, key, index) in filters"
									  v-bind:filter="filter" v-bind:key="key"
									  v-bind:index="index"
									  v-bind:specification="filter.specification">
						</filter-group>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<script>
    import he from 'he';
	import { mapState, mapActions, mapMutations } from 'vuex';
	import FilterGroup from '../ui/filter-group';

	export default {
		props: ['filters'],
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
				this.updateSpecification({ key: 'SearchTerm', value: he.decode(this.query)});
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