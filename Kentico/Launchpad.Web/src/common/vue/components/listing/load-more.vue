<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <div class="center-btn">
        <button class="btn primary-btn large-btn" @click.prevent="changePage(pagination.pageIndex + 1)" v-bind:disabled="pagination.pageIndex >= pagination.totalPages ? 'disabled' : false">Load More</button>
    </div>
</template>

<script>
    import { mapState, mapMutations } from 'vuex';

    export default {
        computed: mapState({
            pagination: state => state.listing.pagination,
            specification: state => state.listing.specification,
        }),
        methods: {
			...mapMutations('listing', { updatePeopleProfileSpecification: 'updatePeopleProfileSpecification' }),
            isCurrentPage(page) {
                return this.pagination.pageIndex === page;
            },
            changePage(page) {
                if (page > this.pagination.totalPages) {
                    page = this.pagination.totalPages;
                }

				this.updatePeopleProfileSpecification({ key: 'PageIndex', value: page - 1 });
            },
        }
    }
</script>