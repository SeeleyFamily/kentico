<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <section class="section">
        <div class="row">
            <div class="col-lg-3">
                <filter-sidebar v-bind:filters="filters" v-bind:title="title"></filter-sidebar>
            </div>

            <div class="col-lg-9" v-if="listing.length">
                <div class="cards--pull-up cards--grid cards">
                    <div class="row">
                        <summary-item v-for="(item, index) in listing" v-bind:item="item" v-bind:key="item.DetailID"></summary-item>
                    </div>
                </div>

                <pagination></pagination>
            </div>

            <div class="cards-no-results col-lg-9" v-if="listing.length < 1">
                <h2>No Results</h2>
                <p>Sorry, there are no results matching your Sort and Filter criteria.</p>
                <a href="javascript:void(0);" v-on:click="resetFilters">Clear Sort and Filters</a>
            </div>
        </div>
    </section>
</template>

<script>
        import SmoothScroll from 'smooth-scroll';
	    import {mapState, mapActions, mapMutations} from 'vuex';
		import Pagination from '../../common/components/listing/pagination-dropdown';
		import SummaryItem from '../../common/components/listing/summary-item';
		import FilterSidebar from '../../common/components/listing/filter-sidebar';
        import resourcefilter from '../filters/resource-filter';

	export default {
            props: ['filterid', 'filtertype', 'result', 'resource', 'title'],
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
            'summary-item': SummaryItem,
            'filter-sidebar': FilterSidebar
    },
		mounted: function () {
            let _this = this;

        // take props and send to vuex store
        this.setInitData(this.result);
        this.setResource(this.resource);

        // import correct filters for this view based on prop value
			if (this.filtertype === 'resourcefilter') {
            this.filters = resourcefilter;
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
			...mapMutations('listing', {setInitData: 'setInitData' }),
			...mapMutations('listing', {setResource: 'setResource' }),
			...mapMutations('listing', { resetSpecification: 'resetSpecification' }),
            resetFilters() {
		        this.resetSpecification();
            }
    },
}
</script>