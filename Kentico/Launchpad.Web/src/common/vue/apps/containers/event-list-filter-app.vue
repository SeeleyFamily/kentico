<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <section class="section">
        <div class="row">
            <div class="col-lg-3">
                <filter-sidebar v-bind:filters="filters" v-bind:title="title"></filter-sidebar>
            </div>

            <div class="col-lg-9" v-if="listing.length">
                <div class="cards--events cards--pull-up cards--texture cards--grid cards">
                    <div class="cards-title js-main-title">Upcoming Events</div>
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
    import SummaryItem from '../../common/components/listing/event-summary-item';
    import FilterSidebar from '../../common/components/listing/filter-sidebar';
    import eventfilter from '../filters/event-filter';

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
		    if (this.filtertype === 'eventfilter') {
                this.filters = eventfilter;
            }

            // after filter comes back, scroll to the top
            let scroll = new SmoothScroll();
			let options = {
                header: '[data-scroll-header]',
                speed: 1000
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

            this.checkForPastEvents();
        },
		methods: {
            ...mapActions('listing', { setFilter: 'setFilter' }),
			...mapMutations('listing', {setInitData: 'setInitData' }),
            ...mapMutations('listing', { setResource: 'setResource' }),
			...mapMutations('listing', { resetSpecification: 'resetSpecification' }),
            checkForPastEvents(sort) {
                setTimeout(() => {
                    $('.cards-separator').remove();
                    let title = 'Upcoming Events';

                    if (sort !== "ZA" && sort !== "AZ") {
                        if ($('.card--past').length) {
                            $('.card--past').first().before('<div class="cards-separator col-12"><h2 class="cards-title">Past Events</h2></div>');
                        }
                    }

                    if (sort === "AZ" || sort === "ZA") {
                        title = "Events";
                    }
                    else {
                        if (!$('.card--upcoming').length) {
                            title = 'Past Events';
                            $('.cards-separator').remove();
                        }
                        else {
                            title = 'Upcoming Events';
                        }
                    }

                    $('.js-main-title').html(title);
                }, 250);
            },
            resetFilters() {
				this.resetSpecification();
            }
        },
        watch: {
            listing: function () {
                let vm = this;
                let sort = this.$store.state.listing.specification.Sort;

                this.checkForPastEvents(sort);
            }
        },
    }
</script>