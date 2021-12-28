<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<fragment>
		<div v-if="listing.length > 1">
			<h2 class="people-profile-related-heading">Recent Articles by This Contributor</h2>

			<div class="cards--grid cards">
				<summary-item v-for="(item, index) in listing" v-bind:item="item" v-bind:key="item.Id"></summary-item>
			</div>

			<load-more v-if="pagination.totalPages > 1 && pagination.totalPages !== pagination.pageIndex"></load-more>
		</div>
	</fragment>
</template>

<script>
	import SmoothScroll from 'smooth-scroll';
	import { mapState, mapActions, mapMutations } from 'vuex';
	import LoadMore from '../../common/components/listing/load-more';
	import SummaryItem from '../../common/components/listing/people-profile-related-item';

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
			'load-more': LoadMore,
			'summary-item': SummaryItem,
		},
		mounted: function () {
			let _this = this;

			// take props and send to vuex store
			this.setInitData(this.result);
			this.setResource(this.resource);

			// if each spec change should trigger a new api call, use this. else manually call setFilter action on button click
			this.$store.subscribe((mutation, state) => {
				if (mutation.type === 'listing/updatePeopleProfileSpecification') {
					this.setPeopleProfileFilter();
				}
			});
		},
		methods: {
			...mapActions('listing', { setPeopleProfileFilter: 'setPeopleProfileFilter' }),
			...mapMutations('listing', { setInitData: 'setInitData' }),
			...mapMutations('listing', { setResource: 'setResource' }),
        },
    }
</script>