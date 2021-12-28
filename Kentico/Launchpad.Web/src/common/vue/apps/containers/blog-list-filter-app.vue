
<template>
	<section>
		<filter-sidebar v-bind:filters="filters"></filter-sidebar>
		<div class="content-list-wrapper">
			<blog-list-item 
				v-for="(item) in listing"
				v-bind:item="item"
				v-bind:key="item.DetailID">
            </blog-list-item>
			<pagination></pagination>
		</div>
	</section>
</template>

<script>
	import SmoothScroll from 'smooth-scroll';
	import { mapState, mapActions, mapMutations } from 'vuex';
	import Pagination from '../../common/components/listing/pagination-dropdown';
	import BlogListItem from '../../common/components/listing/blog-list-item';
	import FilterSidebar from '../../common/components/listing/filter-sidebar';
	import blogFilter from '../filters/blog-filter';

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
			pagination: state => state.listing.pagination,
		}),
		components: {
			pagination: Pagination,
			'blog-list-item': BlogListItem,
	        'filter-sidebar': FilterSidebar
		},
		mounted: function () {
			let _this = this;

			// take props and send to vuex store
			this.setInitData(this.result);
			this.setResource(this.resource);

			// import correct filters for this view based on prop value
			this.filters = blogFilter;

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
			...mapMutations('listing', { resetSpecification: 'resetSpecification' }),
            resetFilters() {
		        this.resetSpecification();
            }
        },
    }
</script>