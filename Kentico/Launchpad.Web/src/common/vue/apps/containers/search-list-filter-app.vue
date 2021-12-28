<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<div class="search-list">
		<search-hero v-bind:filters="filters"></search-hero>

		<section class="search-info">
			<div class="container">
				<div class="row">
					<div class="col-sm-10 offset-sm-1">
						<p>
							Showing
							<b>{{ pagination.rowEnd > 0 ? pagination.rowStart + 1 : 0 }} - {{ pagination.rowEnd > 0 ? pagination.rowEnd : 0 }} of {{ pagination.total }} {{ pagination.total > 1 ? "results" : "result"}}</b> for <b><i>"{{ specification.Query }}</i></b>"
						</p>
					</div>
				</div>
			</div>
		</section>

		<section class="search-list-items section">
			<div class="container" v-if="pagination.total > 0">
				<div class="row">
					<div class="col-sm-10 offset-sm-1">
						<div class="cards cards--list">
							<search-item v-for="(item, index) in listing" v-bind:item="item" v-bind:key="item.DetailID"></search-item>
						</div>
						<pagination></pagination>
					</div>
				</div>
			</div>
			<div v-if="pagination.total == 0">
				<div class="container">
					<h2>No Results</h2>
					<p>
						Sorry, there are no results matching your search criteria.<br />
						Try checking your spelling or broaden your search by entering fewer keywords.
					</p>
				</div>
			</div>
		</section>
	</div>
</template>

<script>
	import SmoothScroll from 'smooth-scroll';
	import { mapState, mapActions, mapMutations } from 'vuex';
	import Pagination from '../../common/components/listing/pagination-dropdown';
	import SearchItem from '../../common/components/search/search-item';
	import SearchHero from '../../common/components/search/search-hero';
	import searchfilter from '../filters/search-filter';

	export default {
		props: ['filterid', 'filtertype', 'result', 'resource'],
		data: function () {
			return {
				filters: {},
			}
		},
		computed: mapState({
			loading: state => state.listing.loading,
			listing: state => state.listing.listing,
			specification: state => state.listing.specification,
			pagination: state => state.listing.pagination,
		}),
		components: {
			pagination: Pagination,
			'search-item': SearchItem,
			'search-hero': SearchHero
		},
		mounted: function () {
			let _this = this;

			// take props and send to vuex store
			this.setInitData(this.result);
			this.setResource(this.resource);

			// import correct filters for this view based on prop value
			if (this.filtertype === 'searchfilter') {
				this.filters = searchfilter;
			}

			// after filter comes back, scroll to the top
			let scroll = new SmoothScroll();
			let options = {
				header: '[data-scroll-header]',
                speed: 1000,
				offset: 96
			};

			// after any api calls, scroll to the top of the list
			this.$store.subscribeAction((action, state) => {
				if (action.type === 'listing/setFilter') {
					let anchor = document.querySelector('#' + _this.filterid);
					let header = document.getElementsByClassName('header')[0];

					scroll.animateScroll(anchor, null, options);

					setTimeout(() => {
						scroll.cancelScroll();
					}, 2000);
				}
			})

			// if each spec change should trigger a new api call, use this. else manually call setFilter action on button click
			this.$store.subscribe((mutation, state) => {
				if (mutation.type === 'listing/updateSpecification') {
					this.setFilter();
				}
			});
		},
		methods: {
			...mapActions('listing', { setFilter: 'setFilter' }),
			...mapMutations('listing', { setInitData: 'setInitData' }),
			...mapMutations('listing', { setResource: 'setResource' }),
        },
    }
</script>